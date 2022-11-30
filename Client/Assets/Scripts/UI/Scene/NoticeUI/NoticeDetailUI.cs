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

    //한줄씩 넘겨받아 처리할 때
    public void SetDetailTexts(string[] DetailTexts)
    {
        m_DetailText.text = null;

        for(int i = 0;i< DetailTexts.Length;++i)
        {
            m_DetailText.text += DetailTexts[i];
            m_DetailText.text += "\n";
        }
    }

    //통째로 처리할 때
    public void SetDetailText(string DetailText)
    {
        //혹시 모르니 초기화
        m_DetailText.text = null;
        m_DetailText.text = DetailText;
    }

    public void ExitButtonCallback()
    {
        m_RectTransform.SetParent(null);
    }

}
