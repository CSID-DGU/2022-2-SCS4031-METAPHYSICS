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
        EightPathScene,
        MyeonJinIndoorScene,
        BongwanIndoor,
        ManhaeGwangjang,
        WonHeungGwan,
        End
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

    public enum Neighbor_Dir
    {
        ND_Top,
        ND_RightTop,
        ND_Right,
        ND_RightBottom,
        ND_Bottom,
        ND_LeftBottom,
        ND_Left,
        ND_LeftTop,
        ND_End
    };

    public enum NavInsert_Type
    {
        None,
        Open,
        Used
    }

}
