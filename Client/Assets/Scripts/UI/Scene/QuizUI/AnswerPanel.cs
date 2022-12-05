using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;

public class AnswerPanel : MonoBehaviour
{
    [SerializeField]
    Text m_AnswerText;

    string m_StrAnswer = null;

    [SerializeField]
    Image m_AcoImage;

    [SerializeField]
    Sprite m_AcoOImage;

    [SerializeField]
    Sprite m_AcoXImage;

    [SerializeField]
    Text m_IsAnswerText;

    [SerializeField]
    Text m_WaitText;

    [SerializeField]
    GameObject m_QuizPanelPrefab;

    public OXMark m_Answer = OXMark.O;

    public float m_WaitTime = 0.0f;
    public float m_WaitTimeMax = 5.0f;

    public string AnswerText
    {
        get
        {
            return m_StrAnswer;
        }

        set
        {
            m_StrAnswer = value;
        }
    }

    public OXMark Answer
    {
        get
        {
            return m_Answer;
        }

        set
        {
            m_Answer = value;
        }

    }

    void Start()
    {
        //���ð� ����
        m_WaitTime = m_WaitTimeMax;

        QuizNPC NPC = GameObject.Find("QuizNPC").GetComponent<QuizNPC>();
        QuizData Data = NPC.GetCurrentQuizData();
        m_Answer = Data.Answer;
        m_AnswerText.text = Data.strAnswer;

        if (m_Answer == OXMark.O)
            m_AcoImage.sprite = m_AcoOImage;

        else if (m_Answer == OXMark.X)
            m_AcoImage.sprite = m_AcoXImage;

        if(m_StrAnswer != null)
             m_AnswerText.text = m_StrAnswer;

        NPC.GoNextQuiz();//���� ī��Ʈ ����

        string SoundName = null;

        if(NPC.UserAnswer == m_Answer)
        {
            SoundName = "Correct_Sound_1";
            m_IsAnswerText.text = "����!";
            //������� Ƚ�� +1;
            NPC.PlusAnswerCount();
        }

        else
        {
            SoundName = "Wrong_Sound_1";
            m_IsAnswerText.text = "����!";
        }
        
        Managers.Sound.PlayByName(SoundName, Sound.UIEffect);
    }

    void Update()
    {
        m_WaitTime -= Time.deltaTime;

        QuizNPC NPC = GameObject.Find("QuizNPC").GetComponent<QuizNPC>();
        bool IsFinishQuiz = NPC.IsFinishQuizEvent();

        string strWaitTime = ((int)m_WaitTime + 1).ToString();

        if(IsFinishQuiz)
            m_WaitText.text = strWaitTime + "�� �Ŀ� ��� ����˴ϴ�.";

        else
            m_WaitText.text = strWaitTime + "�� �Ŀ� ���� ������ �����˴ϴ�.";

        if (m_WaitTime < 0.0f)
        {

            if(!IsFinishQuiz)
            {
                GameObject QuestionObj = Instantiate(m_QuizPanelPrefab);
            }

            else
            {
                NPC.QuizGameFinish();
            }

            Destroy(gameObject);
        }
    }
}
