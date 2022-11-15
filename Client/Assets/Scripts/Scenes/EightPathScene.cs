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

        Screen.SetResolution(1920, 1080, false);
        //Screen.SetResolution(640, 480, false);
    }

    public override void Clear()
    {

    }
}
