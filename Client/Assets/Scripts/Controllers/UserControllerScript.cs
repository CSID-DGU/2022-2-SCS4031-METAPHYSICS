using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UserControllerScript : MonoBehaviour
{
	public int Id { get; set; }
	// Start is called before the first frame update
	Animator m_Animator;
	Rigidbody2D m_Rigid;
	Vector2 m_SynchroPos = new Vector2(0.0f, 0.0f);
	bool m_IsSyncPos = true;

	protected bool _updated = false; //패킷 업데이트

	PositionInfo _positionInfo = new PositionInfo();
	public PositionInfo PosInfo
	{
		get { return _positionInfo; }
		set
		{
			if (_positionInfo.Equals(value))
				return;

			_positionInfo = value;
			m_MoveDir = value.Movedir;
			CellPos = new Vector2(value.PosX, value.PosY);
			m_vMoveDir = new Vector2(value.MovedirX, value.MovedirY);
		}
	}

	public void SyncPos()
    {
		Vector2 destPos = CellPos;
		transform.position = destPos;
    }

	[SerializeField]
	public Vector2 CellPos //이거 서버 전달
	{
		get
		{
			return new Vector2(PosInfo.PosX, PosInfo.PosY);
		}

		set
		{
			PosInfo.PosX = value.x;
			PosInfo.PosY = value.y;
			_updated = true;
		}
	}

	[SerializeField]
	public Vector2	m_vMoveDir //이거 서버 전달
	{
		get
		{
			return new Vector2(PosInfo.MovedirX, PosInfo.MovedirY);
		}

		set
		{
			PosInfo.MovedirX = value.x;
			PosInfo.MovedirY = value.y;
			_updated = true;
		}
	}

	[SerializeField]
	public int m_MoveDir //애니메이션, 서버 전달
    {
        get
        {
			return PosInfo.Movedir;
		}
        set
        {
			PosInfo.Movedir = value;
			_updated = true;
        }
    }

	[SerializeField]
	private float	m_MoveSpeed;

	//[SerializeField]
	//private bool	m_IsMoving = false;

	[SerializeField]
	private GameObject m_FriendListPrefab = null;
	bool m_FriendListON = false;

	protected virtual void Start()
	{
		UserCustomize UserColor = Managers.Data.GetCurrentUserColor();

		string AnimationPath = "";

		if (Managers.Data.GetCurrentPrivilege() == UserPrivileges.Guest)
			AnimationPath = "Animations\\Guest\\GuestAnimController";

		else
		{
			AnimationPath = "Animations\\ACO\\";

			switch (UserColor)
			{
				case UserCustomize.Red:
					AnimationPath += "Red\\";
					break;
				case UserCustomize.Orange:
					AnimationPath += "Orange\\";
					break;
				case UserCustomize.Yellow:
					AnimationPath += "Yellow\\";
					break;
				case UserCustomize.Green:
					AnimationPath += "Green\\";
					break;
				case UserCustomize.Pink:
					AnimationPath += "Pink\\";
					break;
				case UserCustomize.SkyBlue:
					AnimationPath += "SkyBlue\\";
					break;
				case UserCustomize.Navy:
					AnimationPath += "Navy\\";
					break;
				case UserCustomize.Black:
					AnimationPath += "Black\\";
					break;
				case UserCustomize.End:
					AnimationPath += "Black\\";
					break;
			}

			AnimationPath += "ACOAnimController";

		}


		m_Animator = GetComponent<Animator>();
		m_Rigid = GetComponent<Rigidbody2D>();
		m_MoveSpeed = 4.0f;
		m_vMoveDir = new Vector2(0.0f, 0.0f);
		transform.position = new Vector3Int(0, 0, 0);

		Text UserText = gameObject.GetComponentInChildren<Text>();
		UserText.text = Managers.Data.GetCurrentUser();

		RuntimeAnimatorController Cont = Resources.Load<RuntimeAnimatorController>(AnimationPath);

		m_Animator.runtimeAnimatorController 
			= RuntimeAnimatorController.Instantiate(Resources.Load<RuntimeAnimatorController>(AnimationPath));

	}

	protected virtual void FixedUpdate()
    {
        if (!m_IsSyncPos)
        {
            Vector2 Dir = m_SynchroPos - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Vector2 Velocity = Dir * Time.deltaTime;
			m_Rigid.MovePosition(m_SynchroPos);
		}

        else
            gameObject.transform.position = m_SynchroPos;
    }

	protected virtual void Update()
	{
		//GetInput();
		OtherUpdatePosition();
		//UpdateIsMoving();

		
	}

	protected virtual void LateUpdate()
	{
		UpdateAnimation();

		//Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

    protected virtual void GetInput()
    {
		Vector2 MoveDir = m_vMoveDir;
		//추후에 길찾기 구현 후 마우스 이동으로 수정 예정
		if (Input.GetKey(KeyCode.W))
        {
			MoveDir.y = 1.0f;
        }

        else if (Input.GetKey(KeyCode.S))
        {
			MoveDir.y = -1.0f;
        }

        else
        {
			MoveDir.y = 0.0f;
            m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 0.0f);
        }

        if (Input.GetKey(KeyCode.A))
        {
			MoveDir.x = -1.0f;
        }

        else if (Input.GetKey(KeyCode.D))
        {
			MoveDir.x = 1.0f;
        }

        else
        {
			MoveDir.x = 0.0f;
            m_Rigid.velocity = new Vector2(0.0f, m_Rigid.velocity.y);
        }

		m_vMoveDir = MoveDir;
    }

	protected virtual void UpdatePosition()
    {
		float VelocityX = 0.0f;
		float VelocityY = 0.0f;

		if (m_vMoveDir.x == 1.0f)
			VelocityX = m_MoveSpeed;

		else if (m_vMoveDir.x == -1.0f)
			VelocityX = -m_MoveSpeed;

		else 
			VelocityX = 0.0f;


		if (m_vMoveDir.y == 1.0f)
			VelocityY = m_MoveSpeed;

		else if (m_vMoveDir.y == -1.0f)
			VelocityY = -m_MoveSpeed;

		else
			VelocityY = 0.0f;

		m_Rigid.velocity = new Vector2(VelocityX, VelocityY);
		Vector2 destPos = CellPos;
		destPos.x = m_Rigid.transform.position.x;
		destPos.y = m_Rigid.transform.position.y;
		CellPos = destPos;
	}

	protected virtual void OtherUpdatePosition()
	{
		float VelocityX = 0.0f;
		float VelocityY = 0.0f;

		if (m_vMoveDir.x == 1.0f)
			VelocityX = m_MoveSpeed;

		else if (m_vMoveDir.x == -1.0f)
			VelocityX = -m_MoveSpeed;

		else
			VelocityX = 0.0f;


		if (m_vMoveDir.y == 1.0f)
			VelocityY = m_MoveSpeed;

		else if (m_vMoveDir.y == -1.0f)
			VelocityY = -m_MoveSpeed;

		else
			VelocityY = 0.0f;

		m_SynchroPos = new Vector2(PosInfo.PosX, PosInfo.PosY);

		m_IsSyncPos = 0.1f > (Vector2.Distance(m_SynchroPos, gameObject.transform.position));

		//gameObject.transform.position = m_SynchroPos;
		m_MoveDir = PosInfo.Movedir;
	}

	protected virtual void UpdateIsMoving()
    {
		if (m_Rigid.velocity == Vector2.zero)
		{
			//m_IsMoving = false;
			m_MoveDir = (int)MoveDir.None;
		}

		else
        {
			//m_IsMoving = true;

			if(m_vMoveDir.x == 1.0f)
            {
				if (m_vMoveDir.y == 1.0f)
					m_MoveDir = (int)MoveDir.UpRight;

				else if (m_vMoveDir.y == -1.0f)
					m_MoveDir = (int)MoveDir.DownRight;

				else
					m_MoveDir = (int)MoveDir.Right;
            }

			else if (m_vMoveDir.x == -1.0f)
			{
				if (m_vMoveDir.y == 1.0f)
					m_MoveDir = (int)MoveDir.UpLeft;

				else if (m_vMoveDir.y == -1.0f)
					m_MoveDir = (int)MoveDir.DownLeft;

				else
					m_MoveDir = (int)MoveDir.Left;

			}

			if (m_vMoveDir.y == 1.0f)
			{
				if (m_vMoveDir.x == 1.0f)
					m_MoveDir = (int)MoveDir.UpRight;

				else if (m_vMoveDir.x == -1.0f)
					m_MoveDir = (int)MoveDir.UpLeft;

				else
					m_MoveDir = (int)MoveDir.Up;
			}

			else if (m_vMoveDir.y == -1.0f)
			{
				if (m_vMoveDir.x == 1.0f)
					m_MoveDir = (int)MoveDir.DownRight;

				else if (m_vMoveDir.x == -1.0f)
					m_MoveDir = (int)MoveDir.DownLeft;

				else
					m_MoveDir = (int)MoveDir.Down;

			}
		}


	}

	void UpdateAnimation()
	{
        switch (m_MoveDir)
        {
            case (int)MoveDir.None:
				m_Animator.Play("IDLE_FRONT");
				break;
            case (int)MoveDir.Up:
				m_Animator.Play("WALK_BACK");
				break;
            case (int)MoveDir.Down:
				m_Animator.Play("WALK_FRONT");
				break;
            case (int)MoveDir.Left:
				m_Animator.Play("WALK_LEFT");
				break;
            case (int)MoveDir.Right:
				m_Animator.Play("WALK_RIGHT");
				break;
            case (int)MoveDir.UpRight:
				m_Animator.Play("WALK_RIGHTUP");
				break;
            case (int)MoveDir.UpLeft:
				m_Animator.Play("WALK_LEFTUP");
				break;
            case (int)MoveDir.DownRight:
				m_Animator.Play("WALK_RIGHTDOWN");
				break;
            case (int)MoveDir.DownLeft:
				m_Animator.Play("WALK_LEFTDOWN");
				break;
        }

    }

}
