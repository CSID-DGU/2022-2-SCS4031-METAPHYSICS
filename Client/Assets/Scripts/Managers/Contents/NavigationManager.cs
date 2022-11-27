using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;
using static Define;

public class NavigationManager
{
    [SerializeField]
    Grid m_CurrentGrid = null;

    Navigation m_Navigation = new Navigation();
	Queue<NavData> m_NavMsgQueue = new Queue<NavData>();
    List<List<PortalData>> m_WayPointList = new List<List<PortalData>>();

    Stack<PortalData> m_PortalPathStack = new Stack<PortalData>();


    public void Init()
    {
        SetWayPointData();
    }

    public void update()
    {
        while(m_NavMsgQueue.Count != 0)
        {
            //NavData
        }
    }

    public bool FindPath(string CurrentSceneName, string DestSceneName, bool First)
    {
        m_PortalPathStack.Clear();
        
        FindPath(CurrentSceneName, DestSceneName);

        return true;
    }
    public bool FindPath(string CurrentSceneName, string DestSceneName)
    {
        List<PortalData> CurrentPortalList = new List<PortalData>();

        //현재 씬에서 이동 가능한 포탈을 찾는다.
        for (int i = 0; i < (int)GameScene.End; ++i)
        {
            if (((GameScene)i).ToString() == CurrentSceneName)
            {
                for (int j = 0; j < m_WayPointList[i].Count; ++j)
                {
                    CurrentPortalList.Add(m_WayPointList[i][j]);
                }
            }
        }

        PortalData Data = new PortalData();
        Data.CurrentScene = GameScene.End;

        for (int i = 0; i < CurrentPortalList.Count; ++i) 
        {
            m_PortalPathStack.Clear();

            Data = CurrentPortalList[i];
            GameScene NextScene = Data.NextSceneType;

            if (NextScene.ToString() == DestSceneName)
            {
                m_PortalPathStack.Push(CurrentPortalList[i]);
                return true;
            }

            int Index = (int)NextScene;

            bool IsComplete = false;
            string PrevScene = new string(CurrentSceneName);
            IsComplete = CheckNextPortalList(Index, NextScene.ToString(), DestSceneName, ref PrevScene);

            if (!IsComplete)
                break;
        }

        if(Data.CurrentScene != GameScene.End)
            m_PortalPathStack.Push(Data);

        return true;
    }

    public bool CheckNextPortalList(int Index, string Src, string Dest , ref string PrevScene)
    {
        for (int i = 0; i < m_WayPointList[Index].Count; ++i)
        {
            GameScene NextScene = m_WayPointList[Index][i].NextSceneType;

            if (NextScene.ToString() == Dest)
                m_PortalPathStack.Push(m_WayPointList[Index][i]);

            else
            {
                if (NextScene.ToString() == Src || NextScene.ToString() == PrevScene)
                    continue;

                PrevScene = new string(Src);
                CheckNextPortalList((int)NextScene, NextScene.ToString(), Dest, ref PrevScene);
            }
        }

        return true;
    }

    public bool IsNavEnable()
    {
        //true 리턴시 길찾기 해야한다 
        return (m_PortalPathStack.Count != 0);
    }

    public PortalData GetCurrentNavPortal()
    {
        return m_PortalPathStack.Peek();
    }
    public void SetCompleteCurrentNav()
    {
        m_PortalPathStack.Pop();
    }
    
    void SetWayPointData()
    {
        
        for(int i = 0;i<(int)GameScene.End;++i)
        {
            List<PortalData> List = new List<PortalData>();
            m_WayPointList.Add(List);
        }

        //팔정도에 존재하는 Portal정보세팅
        PortalData Data = new PortalData();
        Data.CurrentScene = GameScene.EightPathScene;
        Data.NextSceneType = GameScene.MyeonJinIndoorScene;
        m_WayPointList[(int)GameScene.EightPathScene].Add(Data);

        Data = new PortalData();
        Data.CurrentScene = GameScene.EightPathScene;
        Data.NextSceneType = GameScene.BongwanIndoor;
        m_WayPointList[(int)GameScene.EightPathScene].Add(Data);

        Data = new PortalData();
        Data.CurrentScene = GameScene.EightPathScene;
        Data.NextSceneType = GameScene.ManhaeOutScene;
        m_WayPointList[(int)GameScene.EightPathScene].Add(Data);

        //명진관 실내에 존재하는 Portal 정보 세팅
        Data = new PortalData();
        Data.CurrentScene = GameScene.MyeonJinIndoorScene;
        Data.NextSceneType = GameScene.EightPathScene;
        m_WayPointList[(int)GameScene.MyeonJinIndoorScene].Add(Data);

        //본관 실내에 존재하는 Portal 정보 세팅
        Data = new PortalData();
        Data.CurrentScene = GameScene.BongwanIndoor;
        Data.NextSceneType = GameScene.EightPathScene;
        m_WayPointList[(int)GameScene.BongwanIndoor].Add(Data);

        //만해광장에 존재하는 Portal 정보 세팅
        Data = new PortalData();
        Data.CurrentScene = GameScene.ManhaeOutScene;
        Data.NextSceneType = GameScene.EightPathScene;
        m_WayPointList[(int)GameScene.ManhaeOutScene].Add(Data);


        Data = new PortalData();
        Data.CurrentScene = GameScene.ManhaeOutScene;
        Data.NextSceneType = GameScene.WonHeungIndoor;
        m_WayPointList[(int)GameScene.ManhaeOutScene].Add(Data);

        //원흥관에 존재하는 Portal 정보 세팅

        Data = new PortalData();
        Data.CurrentScene = GameScene.WonHeungIndoor;
        Data.NextSceneType = GameScene.ManhaeOutScene;
        m_WayPointList[(int)GameScene.WonHeungIndoor].Add(Data);

    }
}
