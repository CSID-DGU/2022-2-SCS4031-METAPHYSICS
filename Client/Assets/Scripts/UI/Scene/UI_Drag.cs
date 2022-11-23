using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using UnityEditor;

public class UI_Drag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    //string m_UIName = null;

    static UI_Drag CurrentTopUI = null;
    static int DragUISortingOroder = 1;

    [SerializeField]
    protected RectTransform m_PanelRect = null;

    [SerializeField]
    bool m_bMouseClick = false;

    Vector2 m_WidgeMouseClickOffset = new Vector2(0.0f, 0.0f);

    void Start()
    {
        Managers.UI.AddDragUICount();
        int Count = Managers.UI.GetDragUICount();
    }

    void OnDestroy()
    {
        if (CurrentTopUI == this)
            CurrentTopUI = null;

        Managers.UI.MinusDragUICount();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CurrentTopUI != null)
            CurrentTopUI.SetSortingOrder(DragUISortingOroder);

        //임의로 가장 큰 값을 넣어줘서 가장 앞에 올 수 있도록 출력
        SetSortingOrder(20);
        CurrentTopUI = this;

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

    public void SetSortingOrder(int Order)
    {
        Canvas CurrentCanvas = gameObject.GetComponentInChildren<Canvas>();

        if (CurrentCanvas == null)
            return;

        CurrentCanvas.sortingOrder = Order;
    }
}
