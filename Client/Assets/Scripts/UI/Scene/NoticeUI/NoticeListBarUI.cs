using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeListBarUI : MonoBehaviour
{
    //NoticeUI만 받는다.
    RectTransform m_OwnerRectTransform = null;

    [SerializeField]
    GameObject m_DetailPageListBarPrefab = null;


    public RectTransform OwnerRectTransform
    {
        get
        {
            return m_OwnerRectTransform;
        }

        set
        {
            m_OwnerRectTransform = value;
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DetailButtonCallback()
    {
        GameObject DetailPageObj = Instantiate(m_DetailPageListBarPrefab);

        NoticeDetailUI DetailUI = DetailPageObj.GetComponent<NoticeDetailUI>();

        DetailUI.Rect.SetParent(m_OwnerRectTransform);
        DetailUI.Rect.position = m_OwnerRectTransform.position;
        DetailUI.Rect.position -= new Vector3(0.0f, 70.0f, 0.0f);
        //크롤링 형식 정해지면 제대로
        //DetailUI.SetDetailTexts
        //DetailUI.SetDetailText
    }
}
