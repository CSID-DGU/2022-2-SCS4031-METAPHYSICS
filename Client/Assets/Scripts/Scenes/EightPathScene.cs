using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EightPathScene : BaseScene
{    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.Map.LoadMap(2);

        Screen.SetResolution(1920, 980, false);
        //Screen.SetResolution(640, 480, false);

        //씬입장
        C_EnterGame enterPacket = new C_EnterGame();
        ////임의 설정
        //enterPacket.PlayerId = 3;
        //enterPacket.Name = "df";
        Managers.Network.Send(enterPacket);
    }

    public override void Clear()
    {

    }
}
