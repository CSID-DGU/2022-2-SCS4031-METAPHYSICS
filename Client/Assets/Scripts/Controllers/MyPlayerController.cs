using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;

public class MyPlayerController : UserControllerScript
{
	bool m_IsDebugNav = false;
	Navigation m_Nav = new Navigation();
	List<Vector3> m_PathList;
	Stack<Vector3> m_PathStack = new Stack<Vector3>();
	bool m_IsAutoMoving = false;
	GameScene m_AutoMovingDest = GameScene.End;
	protected override void Start()
	{
		base.Start();

		//Vector3 InitPos = Managers.Scene.GetPlayerInitPos();
		//gameObject.transform.position = InitPos;

		GameObject[] LoadColliders = GameObject.FindGameObjectsWithTag("SceneLoadCollider");
		GameObject InitPosObj = null;

		for (int i = 0; i < LoadColliders.Length; ++i)
        {
			LoadSceneColliderScript ColliderScript = LoadColliders[i].GetComponent<LoadSceneColliderScript>();

			if(ColliderScript.GetNextScene().ToString() == Managers.Scene.GetPrevSceneName())
            {
				InitPosObj = LoadColliders[i];
				break;
			}
        }

		if (InitPosObj == null)
			return;

		Vector3 InitPos = InitPosObj.transform.position;
		gameObject.transform.position = InitPos;
	}

	protected override void FixedUpdate()
    {

    }
	protected override void Update()
	{
		GetInput();
		UpdatePosition();
		UpdateNavigation();
		UpdateIsMoving();
	}

	

	protected  void UpdateNavigation()
    {
		NavigationManager NavManager = Managers.Navigation;
		string CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

		if (NavManager.IsNavEnable())
        {
			PortalData DestPortal = NavManager.GetCurrentNavPortal();

			if (DestPortal.CurrentScene.ToString() == CurrentSceneName)
            {
				Vector2 PlayerPos = transform.position;

				GameObject[] PortalObjs = GameObject.FindGameObjectsWithTag("SceneLoadCollider");
				GameObject DestPortalObj = null;

				for(int i = 0;i< PortalObjs.Length;++i)
                {
					LoadSceneColliderScript SceneColliderComponent = PortalObjs[i].GetComponent<LoadSceneColliderScript>();

					GameScene NextScene = SceneColliderComponent.GetNextScene();

					if (SceneColliderComponent.GetNextScene() == DestPortal.NextSceneType)
					{
						DestPortalObj = PortalObjs[i];
						m_AutoMovingDest = SceneColliderComponent.GetNextScene();
						break;
					}
                }

				if (DestPortalObj != null)
				{
					m_PathList = new List<Vector3>();
					m_Nav.FindPath(PlayerPos, DestPortalObj.transform.position, ref m_PathList);
					m_PathStack = new Stack<Vector3>();

					for (int i = 0; i < m_PathList.Count; ++i)
					{
						m_PathStack.Push(m_PathList[i]);
					}

					NavManager.SetCompleteCurrentNav();
				}
            }
		}

		if (m_PathStack != null)
		{
			if (m_PathStack.Count != 0)
			{
				m_IsAutoMoving = true;
				Vector2 TargetPos = m_PathStack.Peek();

				float Distance = Vector2.Distance(transform.position, TargetPos);

				if (Distance < 0.1f)
				{
					TargetPos = m_PathStack.Pop();
					Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
					rigid.transform.position = TargetPos;
					rigid.velocity = Vector3.zero;
					m_vMoveDir = Vector2.zero;
				}

				else
				{
					Vector2 Dir = TargetPos - new Vector2(transform.position.x, transform.position.y);
					Dir.Normalize();

					Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();

					rigid.velocity = Dir * 4.0f;
					m_vMoveDir = Dir;
				}
			}

			else
			{
				m_IsAutoMoving = false;
				m_PathStack = null;
				Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
				rigid.velocity = Vector3.zero;
			}
		}
		//CheckUpdatedFlag();
	}
	protected override void LateUpdate()
	{
		base.LateUpdate();

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
		
	}

	// 키보드 입력
	protected override void GetInput()
	{
		//채팅 입력이 활성화 되어있을 경우 움직이지 않는다.
		if (Managers.UI.ChatEnable)
			return;

		base.GetInput();
	}

	protected override void UpdatePosition()
	{
		base.UpdatePosition();

		//CheckUpdatedFlag();
	}

	protected override void UpdateIsMoving()
	{

		if (m_PathStack == null)
		{
			gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
			base.UpdateIsMoving();
		}
		
		else
        {
			gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
			if(m_vMoveDir.x > 0.0f)
            {
				if (m_vMoveDir.y > 0.5f)
					m_MoveDir = (int)MoveDir.UpRight;

				else if (m_vMoveDir.y < -0.5f)
					m_MoveDir = (int)MoveDir.DownRight;

				else 
					m_MoveDir = (int)MoveDir.Right;
            }

			else if (m_vMoveDir.x < 0.0f)
			{
				if (m_vMoveDir.y > 0.5f)
					m_MoveDir = (int)MoveDir.UpLeft;

				else if (m_vMoveDir.y < -0.5f)
					m_MoveDir = (int)MoveDir.DownLeft;

				else
					m_MoveDir = (int)MoveDir.Left;
			}

			if (m_vMoveDir.y > 0.0f)
			{
				if (m_vMoveDir.x > 0.5f)
					m_MoveDir = (int)MoveDir.UpRight;

				else if (m_vMoveDir.x < -0.5f)
					m_MoveDir = (int)MoveDir.UpLeft;

				else 
					m_MoveDir = (int)MoveDir.Up;
			}

			else if (m_vMoveDir.y < 0.0f)
			{
				if (m_vMoveDir.x > 0.5f)
					m_MoveDir = (int)MoveDir.DownRight;

				else if (m_vMoveDir.x < -0.5f)
					m_MoveDir = (int)MoveDir.DownLeft;

				else 
					m_MoveDir = (int)MoveDir.Down;
			}
		}

		PosInfo.Movedir = m_MoveDir;

		CheckUpdatedFlag();
	}

	void CheckUpdatedFlag()
	{
		if (_updated)
		{
			C_Move movePacket = new C_Move();
			movePacket.PosInfo = PosInfo;
			Managers.Network.Send(movePacket);
			_updated = false;
		}
	}

	public bool IsAutoMoving()
    {
		return m_IsAutoMoving;
    }

	public GameScene GetAutoMoveDest()
    {
		return m_AutoMovingDest;
    }
}
