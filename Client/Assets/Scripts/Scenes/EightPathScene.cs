using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EightPathScene : BaseScene
{
    protected bool _updated = false; //패킷 업데이트
    PlayerInfo _playerInfo = new PlayerInfo();
    public PlayerInfo Player_Info
    {
        get { return _playerInfo; }
        set
        {
            if (_playerInfo.Equals(value))
                return;

            _playerInfo = value;
            PlayerID = value.PlayerId;
            UserName = value.UserName;
            ColorIndex = value.ColorIndex;
            PosX = value.PosInfo.PosX;
            PosY = value.PosInfo.PosY;
            MovedirX = value.PosInfo.MovedirX;
            MovedirY = value.PosInfo.MovedirY;
            Scene = value.Scene;
        }
    }
    public string UserName //이거 서버 전달
    {
        get
        {
            return Player_Info.UserName;
        }

        set
        {
            Player_Info.UserName = value;
            _updated = true;
        }
    }
    public string Scene //이거 서버 전달
    {
        get
        {
            return Player_Info.Scene;
        }

        set
        {
            Player_Info.Scene = value;
            _updated = true;
        }
    }
    public int PlayerID //이거 서버 전달
    {
        get
        {
            return Player_Info.PlayerId;
        }

        set
        {
            Player_Info.PlayerId = value;
            _updated = true;
        }
    }
    public int ColorIndex //이거 서버 전달
    {
        get
        {
            return Player_Info.ColorIndex;
        }

        set
        {
            Player_Info.ColorIndex = value;
            _updated = true;
        }
    }
    public int UserPrivilege //이거 서버 전달
    {
        get
        {
            return Player_Info.UserPrivilege;
        }

        set
        {
            Player_Info.UserPrivilege = value;
            _updated = true;
        }
    }
    public float PosX //이거 서버 전달
    {
        get
        {
            return Player_Info.PosInfo.PosX;
        }

        set
        {
            Player_Info.PosInfo.PosX = value;
            _updated = true;
        }
    }
    public float PosY //이거 서버 전달
    {
        get
        {
            return Player_Info.PosInfo.PosY;
        }

        set
        {
            Player_Info.PosInfo.PosY = value;
            _updated = true;
        }
    }
    public float MovedirX //이거 서버 전달
    {
        get
        {
            return Player_Info.PosInfo.MovedirX;
        }

        set
        {
            Player_Info.PosInfo.MovedirX = value;
            _updated = true;
        }
    }
    public float MovedirY //이거 서버 전달
    {
        get
        {
            return Player_Info.PosInfo.MovedirY;
        }

        set
        {
            Player_Info.PosInfo.MovedirY = value;
            _updated = true;
        }
    }
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.Map.LoadMap(2);

        Screen.SetResolution(1920, 1080, false);
        //Screen.SetResolution(1920, 980, false);
        //Screen.SetResolution(640, 480, false);

        UserName = Managers.Data.GetCurrentUser();
        ColorIndex = Managers.Data.GetCurrentUserColorIndex();
        UserPrivilege = (int)Managers.Data.GetCurrentPrivilege();
        PlayerID = Managers.Data.GetCurrentUserId();
        Scene = Managers.Data.GetCurrentScene();
        //// TODO 위치, 방향 정보
        //PosX = Managers.Data.GetCurrentPosX(1);
        //PosY = Managers.Data.GetCurrentPosY(1);
        //MovedirX = Managers.Data.GetCurrentDirX(1);
        //MovedirY = Managers.Data.GetCurrentDirY(1);
        //오류남

        // 씬입장
        if(!Managers.Data.start)
        {
            C_EnterGame enterPacket = new C_EnterGame();
            enterPacket.Player = Player_Info;
            Managers.Network.Send(enterPacket);
            Managers.Data.start = true;
        }
        else
        {
            // 씬입장전 초기화
            C_LeaveScene leaveScenePacket = new C_LeaveScene();
            leaveScenePacket.Player = Player_Info;
            Managers.Network.Send(leaveScenePacket);

            C_EnterScene enterPacket = new C_EnterScene();
            enterPacket.Player = Player_Info;
            Managers.Network.Send(enterPacket);
        }
    }

    //void CheckUpdatedFlag()
    //{
    //    if (_updated)
    //    {
    //        C_EnterGame enterPacket = new C_EnterGame();
    //        enterPacket.Player = Player_Info;
    //        Managers.Network.Send(enterPacket);
    //        _updated = false;
    //    }
    //}

    public override void Clear()
    {

    }
}
