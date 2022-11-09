using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MyPlayerController : UserControllerScript
{
	bool m_IsDebugNav = false;
	Navigation m_Nav = new Navigation();
	List<Vector3> m_PathList;
	Stack<Vector3> m_PathStack;
	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		GetInput();
		UpdatePosition();
		UpdateIsMoving();

		if(Input.GetKeyDown(KeyCode.A))
        {
			m_IsDebugNav = m_IsDebugNav ? false : true;
        }

		if (m_IsDebugNav)
		{

			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				Camera cam = FindObjectOfType<Camera>();
				Vector2 MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
				Vector2 PlayerPos = transform.position;

				m_PathList = new List<Vector3>();

				m_Nav.FindPath(PlayerPos, MousePos, ref m_PathList);

				m_PathStack = new Stack<Vector3>();

				for (int i = 0; i < m_PathList.Count; ++i)
				{
					m_PathStack.Push(m_PathList[i]);
				}
			}

			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				Camera cam = FindObjectOfType<Camera>();
				Vector2 MousePos = cam.ScreenToWorldPoint(Input.mousePosition);
				Vector2 PlayerPos = transform.position;
				MousePos = new Vector2(0.0f, 30.0f);

				m_PathList = new List<Vector3>();

				m_Nav.FindPath(PlayerPos, MousePos, ref m_PathList);

				m_PathStack = new Stack<Vector3>();

				for (int i = 0; i < m_PathList.Count; ++i)
				{
					m_PathStack.Push(m_PathList[i]);
				}
			}

		}

		if (m_PathStack != null)
		{
			if (m_PathStack.Count != 0)
			{
				Vector2 TargetPos = m_PathStack.Peek();

				float Distance = Vector2.Distance(transform.position, TargetPos);

				if (Distance < 0.1f)
				{
					TargetPos = m_PathStack.Pop();
					Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
					rigid.transform.position = TargetPos;
					rigid.velocity = Vector3.zero;
				}

				else
				{
					Vector2 Dir = TargetPos - new Vector2(transform.position.x, transform.position.y);
					Dir.Normalize();

					Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();

					rigid.velocity = Dir * 4.0f;
					//m_vMoveDir = Dir;
				}
			}

			else
			{
				m_PathStack = null;
				Rigidbody2D rigid = gameObject.GetComponent<Rigidbody2D>();
				rigid.velocity = Vector3.zero;

			}
		}
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	// 키보드 입력
	protected override void GetInput()
	{
		base.GetInput();
	}

	protected override void UpdatePosition()
	{
		base.UpdatePosition();

		CheckUpdatedFlag();
	}

	protected override void UpdateIsMoving()
	{
		base.UpdateIsMoving();
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
}
