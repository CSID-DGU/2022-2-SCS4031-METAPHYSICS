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

		base.Update();
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
}
