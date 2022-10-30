using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class DirectChatUI : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    [SerializeField]
    private GameObject m_MyChatBoxPrefab = null;

    [SerializeField]
    private GameObject m_FriendChatBoxPrefab = null;

    [SerializeField]
    private Image m_ImagePanel = null;

    [SerializeField]
    private UI_EventHandler m_EventHandle = null;

    [SerializeField]
    private string m_OpponentName = null;

    public Text m_OpponentNameText = null;

    private InputField m_MessageInput = null;

    private ScrollRect m_ScrollView = null;

    private ListBar m_OwnerListBar = null;

    [SerializeField]
    bool m_bMouseClick = false;

    Vector2 m_WidgeMouseClickOffset = new Vector2(0.0f, 0.0f);

    int m_MessageCnt = 0;

    void Start()
    {
        m_OpponentNameText = GetComponentInChildren<Text>();
        m_MessageInput = GetComponentInChildren<InputField>();
        m_ScrollView = GetComponentInChildren<ScrollRect>();
        m_EventHandle = GetComponent<UI_EventHandler>();

        //RectTransform Recttranform = m_ScrollView.content;

        //Instantiate(m_FriendChatBoxPrefab, Recttranform);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(m_MessageInput.text.Length == 0)
                m_MessageInput.ActivateInputField();

            else
            {
                SendDirectMessage();
            }
        }

        //친구 채팅 테스트
        if (m_MessageInput.text.Length == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                ReceiveDirectMessage("Test Message");

            //else if (Input.GetKeyDown(KeyCode.W))
            //    ReceiveDirectMessage("Test Message\nTest Message\nTest Message\nTest Message\nTest Message\nTest Message");
        }

    }

    private void LateUpdate()
    {
        if (m_MessageCnt != m_ScrollView.content.childCount)
        {
            m_MessageCnt = m_ScrollView.content.childCount;

            m_ScrollView.verticalScrollbar.value = 0;

        }

        GUI.FocusControl(null);
    }

    public void SetOpponentName(string Name)
    {
        m_OpponentName = Name;
        m_OpponentNameText.text = Name;

    }

    public void SetOwnerListBar(ListBar Bar)
    {
        m_OwnerListBar = Bar;
    }

    void SendButtonCallback()
    {

    }

    public void BackButtonCallback()
    {
        m_OwnerListBar.SetChattingDisable();
    }

    void BellButtonCallback()
    {

    }
    public void SendDirectMessage()
    {
        string Message = m_MessageInput.text;
        m_MessageInput.text = null;

        RectTransform ContentRect = m_ScrollView.content;

        GameObject MyChat = Instantiate(m_MyChatBoxPrefab, ContentRect);
        MyChat.GetComponentInChildren<Text>().text = Message;

        ChatBoxArea Area = MyChat.GetComponent<ChatBoxArea>();
        Fit(Area.m_BoxRect);
        Fit(Area.m_AreaRect);
        Fit(ContentRect);

        m_ScrollView.verticalScrollbar.value = 0.0f;
    }

    void Fit(RectTransform Rect)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    }


    void ReceiveDirectMessage(string Message)
    {
        m_MessageInput.text = null;

        RectTransform ContentRect = m_ScrollView.content;

        GameObject MyChat = Instantiate(m_FriendChatBoxPrefab, ContentRect);
        MyChat.GetComponentInChildren<Text>().text = Message;

        ChatBoxArea Area = MyChat.GetComponent<ChatBoxArea>();
        Fit(Area.m_BoxRect);
        Fit(Area.m_AreaRect);
        Fit(ContentRect);

        m_ScrollView.verticalScrollbar.value = 0.0f;
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
        //Vector2 WidgetPos = m_ImagePanel.rectTransform.anchoredPosition;
        //Vector2 WidgetSize = m_ImagePanel.rectTransform.sizeDelta;
        //Vector2 WidgetLB = WidgetPos - WidgetSize / 2.0f;

        //Vector2 MouseScreenPos = eventData.position;

        //float MouseWidgetX = MouseScreenPos.x  - Screen.width /2.0f;
        //float MouseWidgetY = MouseScreenPos.y  - Screen.height /2.0f;

        //Vector2 MouseWidgetPos = new Vector2(MouseWidgetX, MouseWidgetY);
        //MouseWidgetPos *= 2.0f;

        //m_WidgeMouseClickOffset = (MouseWidgetPos - WidgetLB) / WidgetSize;

        //m_ImagePanel.rectTransform.anchoredPosition = MouseWidgetPos + m_WidgeMouseClickOffset;

    }
}
