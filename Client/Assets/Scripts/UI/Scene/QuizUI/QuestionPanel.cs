using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;

public class QuestionPanel : MonoBehaviour
{
    [SerializeField]
    GameObject m_AnswerWidgetPrefab;

    [SerializeField]
    Slider m_Slider;

    [SerializeField]
    Text m_QuestionText;

    [SerializeField]
    public OXMark m_Answer = OXMark.O;

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

    float m_Time = 0.0f;
    float m_TimeMax = 5.0f;

    void Start()
    {

        QuizNPC NPC = GameObject.Find("QuizNPC").GetComponent<QuizNPC>();
        QuizData Data = NPC.GetCurrentQuizData();

        m_QuestionText.text = Data.strQuestion;
        m_Answer = Data.Answer;

        m_Time = m_TimeMax;
        m_Slider.maxValue = m_TimeMax;
        m_Slider.value = m_TimeMax;
        m_Slider.minValue = 0.0f;

        Managers.Sound.PlayByName("Timer", Sound.UIEffect);
    }

    void Update()
    {
        if(m_Time > 0.0f)
        {
            m_Time -= Time.deltaTime;

            m_Slider.value = m_Time;
        }

        else
        {
            m_Time = 0.0f;

            Destroy(gameObject);
            GameObject AnswerObj = Instantiate(m_AnswerWidgetPrefab);
            AnswerPanel AnswerWidget = AnswerObj.GetComponent<AnswerPanel>();

            AnswerWidget.Answer = m_Answer;

        }
    }
}
