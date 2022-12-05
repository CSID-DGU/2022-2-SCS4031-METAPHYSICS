using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizPrepareWidget : MonoBehaviour
{
    [SerializeField]
    private Text m_TimeText = null;


    [SerializeField]
    private GameObject m_QuestionWidgetPrefab = null;

    private float m_Time;
    private float m_TimeMax = 6.0f;

    void Start()
    {
        m_Time = m_TimeMax;

        string TimeText = null;
        TimeText += ((int)m_Time).ToString();
        TimeText += " 초 뒤에 퀴즈 이벤트가 시작됩니다.";

        m_TimeText.text = TimeText;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Time > 1.0f)
        {
            m_Time -= Time.deltaTime;
        }

        else
        {
            //퀴즈 인스턴스 생성
            Destroy(gameObject);

            GameObject QuizObj = Instantiate(m_QuestionWidgetPrefab);
            //최초 퀴즈 내용 지정;

        }


        string TimeText = null;
        TimeText += ((int)m_Time).ToString();
        TimeText += " 초 뒤에 퀴즈 이벤트가 시작됩니다.";

        m_TimeText.text = TimeText;
    }
}
