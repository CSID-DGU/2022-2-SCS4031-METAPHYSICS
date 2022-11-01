using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum MoveDir
	{
        None,
        Up,
        Down,
        Left,
        Right,
        UpRight,
        UpLeft,
        DownRight,
        DownLeft
	}

    public enum UserPrivileges
    {
        None,
        Student,
        Guest
    }

    public enum UserCustomize
    {
        Red,
        Orange,
        Yellow,
        Green,
        Pink,
        SkyBlue,
        Navy,
        Black,
        End
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum GameScene
    {
        None,
        EightPathScene,
        MyeonJinIndoorScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
    }
}
