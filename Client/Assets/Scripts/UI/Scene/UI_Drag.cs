using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class UI_Drag : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    // Start is called before the first frame update
    //string m_UIName = null;

    [SerializeField]
    protected RectTransform m_PanelRect = null;

    [SerializeField]
    bool m_bMouseClick = false;

    Vector2 m_WidgeMouseClickOffset = new Vector2(0.0f, 0.0f);

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Vector2 WidgetPos = m_ImagePanel.rectTransform.anchoredPosition;
        //Vector2 WidgetSize = m_ImagePanel.rectTransform.sizeDelta;
        //Vector2 WidgetLB = WidgetPos - WidgetSize / 2.0f;

        //Vector2 MouseScreenPos = eventData.position;

        //float MouseWidgetX = MouseScreenPos.x - Screen.width / 2.0f;
        //float MouseWidgetY = MouseScreenPos.y - Screen.height / 2.0f;

        //Vector2 MouseWidgetPos = new Vector2(MouseWidgetX, MouseWidgetY);

        //m_WidgeMouseClickOffset = (WidgetLB - MouseWidgetPos);

        //m_ImagePanel.rectTransform.anchoredPosition = MouseWidgetPos - m_WidgeMouseClickOffset;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        m_PanelRect.anchoredPosition += eventData.delta;

        Vector2 ScreenSize = new Vector2(Screen.width, Screen.height);
        ScreenSize /= 2.0f;

        Vector2 WidgetPos = m_PanelRect.anchoredPosition;
        Vector2 WidgetSize = m_PanelRect.sizeDelta;

        float UILeft, UIRight, UITop, UIBottom;

        UILeft = WidgetPos.x - WidgetSize.x / 2.0f;
        UIRight = WidgetPos.x + WidgetSize.x / 2.0f;
        UITop = WidgetPos.y + WidgetSize.y / 2.0f;
        UIBottom = WidgetPos.y - WidgetSize.y / 2.0f;

        if (ScreenSize.x < UIRight)
        {
            WidgetPos.x = ScreenSize.x - WidgetSize.x / 2.0f;
        }

        if(ScreenSize.y < UITop)
        {
            WidgetPos.y = ScreenSize.y - WidgetSize.y / 2.0f;
        }

        if(-ScreenSize.x > UILeft)
        {
            WidgetPos.x = -ScreenSize.x + WidgetSize.x / 2.0f;
        }

        if (-ScreenSize.y > UIBottom)
        {
            WidgetPos.y = -ScreenSize.y + WidgetSize.y / 2.0f;

        }

        m_PanelRect.anchoredPosition = WidgetPos;

    }
}
