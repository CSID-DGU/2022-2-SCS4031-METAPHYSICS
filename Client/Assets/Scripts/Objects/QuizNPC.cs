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

    //���� ����
    public int m_QuestionCount = 3;
    public int m_CurrentQuizCount = 0;

    //NPC�� ������ ������ ó����
    OXMark m_UserAnswer = OXMark.None;

    //������ ���� ���� Ƚ��
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
        m_QuizData[0].strQuestion = "���� �������б��� ������ ����(���), �ں�(���), ����(����)�̴�.";
        m_QuizData[0].strAnswer = "�������б��� ������ ����(���), �ں�(���), ����(����)���� 2017�⿡ �缱���Ǿ���.";
        m_QuizData[0].Answer = OXMark.O;

        m_QuizData[1].strQuestion = "�������б��� 108����� ������ 108���̴�.";
        m_QuizData[1].strAnswer = "�������б��� 108����� ������ ��Ȯ�� 108���� 108�������� ��Ƽ�긦 ����ٰ� �Ѵ�.";
        m_QuizData[1].Answer = OXMark.O;

        m_QuizData[2].strQuestion = "�������б��� �ּҴ� ����Ư���� �߱� ��ȣ�� 241�̴�.";
        m_QuizData[2].strAnswer = "�������б��� �ּҴ� ����Ư���� �߱� �ʵ��� 1�� 30�̴�. ����� ����Ư���� �߱� ��ȣ�� 241�� ����ü������ �ּ��̴�.";
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

        //��� ���� ī�޶���
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

        // ������ ��� �÷��̾���� Ŭ�󿡼� ���� ���۵ǵ���
        C_Startminigame minigamePacket = new C_Startminigame();
        minigamePacket.Player.UserName = Managers.Data.GetCurrentUser();
        Managers.Network.Send(minigamePacket);
    }

    public void QuizGameStart()
    {
        //Ȥ�ø𸣴� �ʱ�ȭ
        m_UserAnswerCount = 0;
        m_CurrentQuizCount = 0;

        Managers.Sound.PlayByName("Question_Start", Define.Sound.UIEffect);
        Managers.Cam.CamPlayerTrace = false;
        Managers.Cam.MainCamSize = 6;

        GameObject PrepareObj = Instantiate(m_QuizPreparePrefab);
    }

    public void QuizGameFinish()
    {
        //���⼭
        string UserName = Managers.Data.GetCurrentUser();
        int CorrectCount = m_UserAnswerCount;
        //�� �� ���� ��Ŷ���� ������.

        // ������ ���, �̸� ����
        C_Finishminigame minigamePacket = new C_Finishminigame();
        minigamePacket.UserName = UserName;
        minigamePacket.Score = CorrectCount;
        Managers.Network.Send(minigamePacket);

        //ī�޶� ���� �ʱ�ȭ
        CameraManager CamManager = Managers.Cam;
        CamManager.CamPlayerTrace = true;
        CamManager.InitCameraSize();

        SpawnRankingWidget();

        m_IsQuizStart = false;
        m_UserAnswerCount = 0;
        m_CurrentQuizCount = 0;
    }

    //�������� �� ������ ���� �����͵� ��Ŷ ���� ������ �� �Լ� ȣ�� �ϸ� ��
    public void SpawnRankingWidget()
    {
        GameObject RankingObj = Instantiate(m_QuizRankingWidgetPrefab);
        RankingUI RankingWidget = RankingObj.GetComponentInChildren<RankingUI>();

        //������ �޾Ƽ� ����
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
        //3�������� �Է�
        for (int i = 0; i < 3; ++i)
        {
            m_QuizRankData[i] = Datas[i];
        }

    }

}
