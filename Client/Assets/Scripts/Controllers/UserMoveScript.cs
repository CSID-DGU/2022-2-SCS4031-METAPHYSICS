using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UserMoveScript : MonoBehaviour
{
	// Start is called before the first frame update
	Animator m_Animator;
	Rigidbody2D m_Rigid;

	[SerializeField]
	private Vector2	m_vMoveDir;

	[SerializeField]
	private float	m_MoveSpeed;

	[SerializeField]
	private bool	m_IsMoving = false;

	[SerializeField]
	private MoveDir m_MoveDir = MoveDir.None;

	void Start()
	{
		m_Animator = GetComponent<Animator>();
		m_Rigid = GetComponent<Rigidbody2D>();
		m_MoveSpeed = 4.0f;
		m_vMoveDir = new Vector2(0.0f, 0.0f);
		transform.position = new Vector3Int(0, 0, 0);

		Text UserText = gameObject.GetComponentInChildren<Text>();
		UserText.text = Managers.Data.GetCurrentUser();
	}

	void Update()
	{
        GetInput();
        UpdatePosition();
        UpdateIsMoving();
	}

	void LateUpdate()
	{
		UpdateAnimation();

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	void GetInput()
    {
		//추후에 길찾기 구현 후 마우스 이동으로 수정 예정
		if (Input.GetKey(KeyCode.W))
		{
			m_vMoveDir.y = 1.0f;
		}

        else if(Input.GetKey(KeyCode.S))
        {
			m_vMoveDir.y = -1.0f;
		}
		
		else
		{
			m_vMoveDir.y = 0.0f;
			m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 0.0f);
		}

		if (Input.GetKey(KeyCode.A))
		{
			m_vMoveDir.x = -1.0f;
		}

		else if (Input.GetKey(KeyCode.D))
		{
			m_vMoveDir.x = 1.0f;
		}

		else
		{
			m_vMoveDir.x = 0.0f;
			m_Rigid.velocity = new Vector2(0.0f, m_Rigid.velocity.y);
		}

	}

    void UpdatePosition()
    {
		//m_Rigid.AddForce(m_MoveSpeed * m_vMoveDir);

		//속력 제한
		//if (m_Rigid.velocity.x > 4.0f)
		//    m_Rigid.velocity = new Vector2(4.0f, m_Rigid.velocity.y);

		//else if (m_Rigid.velocity.x < -4.0f)
		//    m_Rigid.velocity = new Vector2(-4.0f, m_Rigid.velocity.y);

		//if (m_Rigid.velocity.y > 4.0f)
		//    m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, 4.0f);

		//else if (m_Rigid.velocity.y < -4.0f)
		//    m_Rigid.velocity = new Vector2(m_Rigid.velocity.x, -4.0f);

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
	}

	void UpdateIsMoving()
    {
		if (m_Rigid.velocity == Vector2.zero)
		{
			m_IsMoving = false;
			m_MoveDir = MoveDir.None;
		}

		else
        {
			m_IsMoving = true;

			if(m_vMoveDir.x == 1.0f)
            {
				if (m_vMoveDir.y == 1.0f)
					m_MoveDir = MoveDir.UpRight;

				else if (m_vMoveDir.y == -1.0f)
					m_MoveDir = MoveDir.DownRight;

				else
					m_MoveDir = MoveDir.Right;
            }

			else if (m_vMoveDir.x == -1.0f)
			{
				if (m_vMoveDir.y == 1.0f)
					m_MoveDir = MoveDir.UpLeft;

				else if (m_vMoveDir.y == -1.0f)
					m_MoveDir = MoveDir.DownLeft;

				else
					m_MoveDir = MoveDir.Left;

			}

			if (m_vMoveDir.y == 1.0f)
			{
				if (m_vMoveDir.x == 1.0f)
					m_MoveDir = MoveDir.UpRight;

				else if (m_vMoveDir.x == -1.0f)
					m_MoveDir = MoveDir.UpLeft;

				else
					m_MoveDir = MoveDir.Up;
			}

			else if (m_vMoveDir.y == -1.0f)
			{
				if (m_vMoveDir.x == 1.0f)
					m_MoveDir = MoveDir.DownRight;

				else if (m_vMoveDir.x == -1.0f)
					m_MoveDir = MoveDir.DownLeft;

				else
					m_MoveDir = MoveDir.Down;

			}
		}

    }

	void UpdateAnimation()
	{
        switch (m_MoveDir)
        {
            case MoveDir.None:
				m_Animator.Play("IDLE_FRONT");
				break;
            case MoveDir.Up:
				m_Animator.Play("WALK_BACK");
				break;
            case MoveDir.Down:
				m_Animator.Play("WALK_FRONT");
				break;
            case MoveDir.Left:
				m_Animator.Play("WALK_LEFT");
				break;
            case MoveDir.Right:
				m_Animator.Play("WALK_RIGHT");
				break;
            case MoveDir.UpRight:
				m_Animator.Play("WALK_RIGHTUP");
				break;
            case MoveDir.UpLeft:
				m_Animator.Play("WALK_LEFTUP");
				break;
            case MoveDir.DownRight:
				m_Animator.Play("WALK_RIGHTDOWN");
				break;
            case MoveDir.DownLeft:
				m_Animator.Play("WALK_LEFTDOWN");
				break;
        }

    }

}
