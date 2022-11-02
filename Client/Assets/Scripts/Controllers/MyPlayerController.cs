using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MyPlayerController : UserControllerScript
{

	protected override void Start()
	{
		base.Start();
	}

	protected override void Update()
	{
		GetInput();
		UpdatePosition();
		UpdateIsMoving();
	}

	protected override void LateUpdate()
	{
		base.LateUpdate();

		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	// Ű���� �Է�
	protected override void GetInput()
	{
		base.GetInput();
	}

	protected override void UpdatePosition()
	{
		Vector2 prevCellPos = CellPos;

		base.UpdatePosition();

		if (CellPos != prevCellPos)
		{
			C_Move movePacket = new C_Move();
			movePacket.PosInfo = PosInfo;
			//�������� ������ ����
			Managers.Network.Send(movePacket);
		}
	}

	protected override void UpdateIsMoving()
	{
		base.UpdateIsMoving();
	}
}
