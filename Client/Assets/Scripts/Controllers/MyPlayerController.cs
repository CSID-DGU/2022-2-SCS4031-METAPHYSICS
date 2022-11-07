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
