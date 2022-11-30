using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeDetailUI : MonoBehaviour
{
    [SerializeField]
    Text m_DetailText = null;

    [SerializeField]
    RectTransform m_RectTransform = null;

    public RectTransform Rect
    {
        get
        {
            return m_RectTransform;
        }

        set
        {
            m_RectTransform = value;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    //���پ� �Ѱܹ޾� ó���� ��
    public void SetDetailTexts(string[] DetailTexts)
    {
        m_DetailText.text = null;

        for(int i = 0;i< DetailTexts.Length;++i)
        {
            m_DetailText.text += DetailTexts[i];
            m_DetailText.text += "\n";
        }
    }

    //��°�� ó���� ��
    public void SetDetailText(string DetailText)
    {
        //Ȥ�� �𸣴� �ʱ�ȭ
        m_DetailText.text = null;
        m_DetailText.text = DetailText;
    }

    public void ExitButtonCallback()
    {
        m_RectTransform.SetParent(null);
    }

}
