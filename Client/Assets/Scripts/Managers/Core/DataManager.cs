using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Struct;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    //public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();

    private UserPrivileges  m_UserPrivilege = UserPrivileges.None;
    private string          m_CurrentUserName = null;
    private UserCustomize m_CurrentUserColor = UserCustomize.End;
    private int m_PrivilegeIndex;
    private int m_ColorIndex;
    private float m_posX;
    private float m_posY;
    private float m_movedirX;
    private float m_movedirY;
    //임시
    private Dictionary<String, UserData> m_UserDataDict = new Dictionary<String, UserData>();
    private List<UserData> m_ArrayUserData = new List<UserData>();

    public void Init()
    {
        //초기 실험용 데이터
        UserData UD = new UserData();
        UD.UserNum = "2017110413";
        UD.Password = "794613";
        UD.UserName = "유재헌";
        UD.UserColor = UserCustomize.Black;

        AddUserData(UD);

        
    }

    public void update()
    {

    }
    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }

    public Struct.UserData FindUserData(String UserNumber)
    {
        UserData UD;

        m_UserDataDict.TryGetValue(UserNumber, out UD);

        return UD;
    }

    public bool ContainsUserName(String UserName)
    {
        UserData UD = new UserData();

        for(int i = 0;i<m_ArrayUserData.Count;++i)
        {
            if (m_ArrayUserData[i].UserName == UserName)
                return true;
        }

        return false;
    }

    public bool IsOverlappedUser(String UserNumber)
    {
        return m_UserDataDict.ContainsKey(UserNumber);
    }

    public bool AddUserData(UserData Data)
    {
        if (Data.UserNum.Length == 0 || Data.Password.Length == 0 || Data.UserName.Length == 0)
            return false;

        m_UserDataDict.Add(Data.UserNum, Data);
        m_ArrayUserData.Add(Data);

        return true;
    }

    public void SetCurrentUser(string UserName)
    {
        m_CurrentUserName = UserName;
    }

    public string GetCurrentUser()
    {
        return m_CurrentUserName;
    }

    public void SetCurrentPrivilege(UserPrivileges UserPrivilege)
    {
        m_UserPrivilege = UserPrivilege;
    }

    public void SetCurrentPrivilege(int UserPrivilege)
    {
        m_PrivilegeIndex = UserPrivilege;
        SetCurrentPrivilege((UserPrivileges)UserPrivilege);
    }

    public UserPrivileges GetCurrentPrivilege()
    {
        return m_UserPrivilege;
    }

    public void SetCurrentUserColor(UserCustomize UserColor)
    {
        m_CurrentUserColor = UserColor;
    }
    public void SetCurrentUserColor(int UserColor)
    {
        if (UserColor >= (int)UserCustomize.End || UserColor < (int)UserCustomize.Red)
            return;

        m_ColorIndex = UserColor;
        SetCurrentUserColor((UserCustomize)UserColor);
    }

    public int GetCurrentUserColorIndex()
    {
        return m_ColorIndex;
    }

    public UserCustomize GetCurrentUserColor()
    {
        return m_CurrentUserColor;
    }

    //장소별 설정
    //public float GetCurrentPosX(int num)
    //{
    //    switch (num)
    //    {
    //        case 0:
    //            m_posX = 0.0f;
    //            break;


    //        case 1:
    //            m_posX = 0.0f;
    //            break;

    //        case 2:
    //            m_posX = 0.0f;
    //            break;

    //        case 3:
    //            m_posX = 0.0f;
    //            break;
    //    }
    //    return m_posX;
    //}

    public float GetCurrentPosX(int num)
    {
        switch (num)
        {
            case 0:
                m_posX = 0.0f;
                break;
               

            case 1:
                m_posX = 0.0f;
                break;

            case 2:
                m_posX = 0.0f;
                break;

            case 3:
                m_posX = 0.0f;
                break;
        }
        return m_posX;
    }

    public float GetCurrentPosY(int num)
    {
        switch (num)
        {
            case 0:
                m_posY = 0.0f;
                break;


            case 1:
                m_posY = 0.0f;
                break;

            case 2:
                m_posY = 0.0f;
                break;

            case 3:
                m_posY = 0.0f;
                break;
        }
        return m_posY;
    }
    public float GetCurrentDirX(int num)
    {
        switch (num)
        {
            case 0:
                m_movedirX = 0.0f;
                break;


            case 1:
                m_movedirX = 0.0f;
                break;

            case 2:
                m_movedirX = 0.0f;
                break;

            case 3:
                m_movedirX = 0.0f;
                break;
        }
        return m_movedirX;
    }
    public float GetCurrentDirY(int num)
    {
        switch (num)
        {
            case 0:
                m_movedirY = 0.0f;
                break;


            case 1:
                m_movedirY = 0.0f;
                break;

            case 2:
                m_movedirY = 0.0f;
                break;

            case 3:
                m_movedirY = 0.0f;
                break;
        }
        return m_movedirY;
    }
}
