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
        TimeText += " �� �ڿ� ���� �̺�Ʈ�� ���۵˴ϴ�.";

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
            //���� �ν��Ͻ� ����
            Destroy(gameObject);

            GameObject QuizObj = Instantiate(m_QuestionWidgetPrefab);
            //���� ���� ���� ����;

        }


        string TimeText = null;
        TimeText += ((int)m_Time).ToString();
        TimeText += " �� �ڿ� ���� �̺�Ʈ�� ���۵˴ϴ�.";

        m_TimeText.text = TimeText;
    }
}
