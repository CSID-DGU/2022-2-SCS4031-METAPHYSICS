using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Struct;
using System.IO;
using Google.Protobuf.Protocol;

public class QuizNPC : MousePickCallbackObj
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject m_QuizStartPopUpPrefab = null;

    [SerializeField]
    GameObject m_QuizRankingWidgetPrefab = null;


    [SerializeField]
    GameObject m_QuizPreparePrefab = null;

    [SerializeField]
    bool m_IsQuizStart = false;
    bool m_QuizPopUpEnable = false;
    GameObject m_PopUpObj = null;

    QuizData[] m_QuizData = new QuizData[3];
    QuizData m_CurrentQuizData;

    QuizRankData[] m_QuizRankData = new QuizRankData[3];

    public Vector3 m_QuizCamPos = new Vector3(-5.3f, -8.5f, -10.0f);

    //문제 갯수
    public int m_QuestionCount = 3;
    public int m_CurrentQuizCount = 0;

    //NPC가 유저의 정답을 처리함
    OXMark m_UserAnswer = OXMark.None;

    //유저가 정답 맞춘 횟수
    public int m_UserAnswerCount = 0;

    public int CurrentQuizCount
    {
        get 
        {
            return m_CurrentQuizCount;
        }
    }

    public void GoNextQuiz()
    {
        ++m_CurrentQuizCount;
    }

    public bool IsFinishQuizEvent()
    {
        bool IsFinish = m_CurrentQuizCount == m_QuestionCount;

        return IsFinish;
    }
    public OXMark UserAnswer
    {
        get
        {
            return m_UserAnswer;
        }

        set
        {
            m_UserAnswer = value;
        }
    }
    

    public bool QuizStart
    {
        get
        {
            return m_IsQuizStart;
        }

        set
        {
            m_IsQuizStart = value;

            if (value)
                SendStartRequest();

        }
    }

    void Start()
    {
        m_QuizData[0].strQuestion = "현재 동국대학교의 교훈은 지혜(智慧), 자비(慈悲), 정진(精進)이다.";
        m_QuizData[0].strAnswer = "동국대학교의 교훈은 지혜(智慧), 자비(慈悲), 정진(精進)으로 2017년에 재선포되었다.";
        m_QuizData[0].Answer = OXMark.O;

        m_QuizData[1].strQuestion = "동국대학교의 108계단의 개수는 108개이다.";
        m_QuizData[1].strAnswer = "동국대학교의 108계단의 개수는 정확히 108개로 108번뇌에서 모티브를 얻었다고 한다.";
        m_QuizData[1].Answer = OXMark.O;

        m_QuizData[2].strQuestion = "동국대학교의 주소는 서울특별시 중구 동호로 241이다.";
        m_QuizData[2].strAnswer = "동국대학교의 주소는 서울특별시 중구 필동로 1길 30이다. 참고로 서울특별시 중구 동호로 241은 장충체육관의 주소이다.";
        m_QuizData[2].Answer = OXMark.X;
    }

    void Update()
    {
        if (m_PopUpObj != null)
        {
            if (!m_PopUpObj.activeInHierarchy)
            {
                m_PopUpObj = null;
            }

        }

        //퀴즈를 위한 카메라세팅
        if (m_IsQuizStart)
            Camera.main.transform.position = m_QuizCamPos;
    }

    public override void MouseClickCallback()
    {
        if (!m_IsQuizStart && m_PopUpObj == null)
        {
            m_PopUpObj = Instantiate(m_QuizStartPopUpPrefab);

            QuizNPCPopUp PopUp = m_PopUpObj.GetComponent<QuizNPCPopUp>();
            PopUp.SetOwnerNPC(this);
        }
    }
    public void SendStartRequest()
    {

        // 씬안의 모든 플레이어들의 클라에서 게임 시작되도록
        C_Startminigame minigamePacket = new C_Startminigame();
        minigamePacket.Player.UserName = Managers.Data.GetCurrentUser();
        Managers.Network.Send(minigamePacket);
    }

    public void QuizGameStart()
    {
        //혹시모르니 초기화
        m_UserAnswerCount = 0;
        m_CurrentQuizCount = 0;

        Managers.Sound.PlayByName("Question_Start", Define.Sound.UIEffect);
        Managers.Cam.CamPlayerTrace = false;
        Managers.Cam.MainCamSize = 6;

        GameObject PrepareObj = Instantiate(m_QuizPreparePrefab);
    }

    public void QuizGameFinish()
    {
        //여기서
        string UserName = Managers.Data.GetCurrentUser();
        int CorrectCount = m_UserAnswerCount;
        //이 두 정보 패킷으로 보낸다.

        // 서버로 기록, 이름 전송
        C_Finishminigame minigamePacket = new C_Finishminigame();
        minigamePacket.UserName = UserName;
        minigamePacket.Score = CorrectCount;
        Managers.Network.Send(minigamePacket);

        //카메라 정보 초기화
        CameraManager CamManager = Managers.Cam;
        CamManager.CamPlayerTrace = true;
        CamManager.InitCameraSize();

        SpawnRankingWidget();

        m_IsQuizStart = false;
        m_UserAnswerCount = 0;
        m_CurrentQuizCount = 0;
    }

    //서버에서 각 유저의 퀴즈 데이터들 패킷 전달 받으면 이 함수 호출 하면 됨
    public void SpawnRankingWidget()
    {
        GameObject RankingObj = Instantiate(m_QuizRankingWidgetPrefab);
        RankingUI RankingWidget = RankingObj.GetComponentInChildren<RankingUI>();

        //데이터 받아서 세팅
        RankingWidget.SetRankData(m_QuizRankData);
    }

    public void PlusAnswerCount()
    {
        ++m_UserAnswerCount;
    }

    public void SetNextQuiz()
    {
        
    }
    public QuizData GetCurrentQuizData()
    {
        return m_QuizData[m_CurrentQuizCount];
    }

    public void SetQuizRankData(QuizRankData[] Datas)
    {
        //3위까지만 입력
        for (int i = 0; i < 3; ++i)
        {
            m_QuizRankData[i] = Datas[i];
        }

    }

}
