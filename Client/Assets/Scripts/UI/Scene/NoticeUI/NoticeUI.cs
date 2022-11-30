using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoticeUI : UI_Drag
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject m_DetailButtonPrefab = null;

    [SerializeField]
    ScrollRect m_ScrollRect = null;

    [SerializeField]
    RectTransform m_RectTransform = null;

    

    void Start()
    {
        //크롤링 해서 정보 세팅

        for(int i = 0;i<6;++i)
        {
            GameObject Obj = Instantiate(m_DetailButtonPrefab, m_ScrollRect.content);

            NoticeListBarUI UI = Obj.GetComponentInChildren<NoticeListBarUI>();
            UI.OwnerRectTransform = m_RectTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
