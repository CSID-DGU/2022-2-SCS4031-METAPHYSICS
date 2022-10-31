using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightPathScene : BaseScene
{    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.Map.LoadMap(2);

        Screen.SetResolution(640, 380, false);
    }

    public override void Clear()
    {

    }
}
