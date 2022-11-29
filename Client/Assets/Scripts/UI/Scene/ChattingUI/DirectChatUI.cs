using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using static Struct;

public class DirectChatUI : UI_Drag
{
    [SerializeField]
    private GameObject m_MyChatBoxPrefab = null;

    [SerializeField]
    private GameObject m_FriendChatBoxPrefab = null;

    //[SerializeField]
    //private Image m_ImagePanel = null;

    [SerializeField]
    private UI_EventHandler m_EventHandle = null;

    [SerializeField]
    private string m_OpponentName = null;

    public Text m_OpponentNameText = null;

    private InputField m_MessageInput = null;

    private ScrollRect m_ScrollView = null;

    private ListBar m_OwnerListBar = null;

    int m_MessageCnt = 0;

    void Start()
    {
        m_OpponentNameText = GetComponentInChildren<Text>();
        m_MessageInput = GetComponentInChildren<InputField>();
        m_ScrollView = GetComponentInChildren<ScrollRect>();
        m_EventHandle = GetComponent<UI_EventHandler>();

        InitMessageList();

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

        //업데이트할 메세지가 있는지 체크하여 업데이트
        if(CheckExistReceiveMessage())
            UpdateReceiveMessage();

        if (m_MessageInput.isFocused)
            Managers.UI.ChatEnable = true;

        else
        {
            Managers.UI.ChatEnable = false;
        }

        //친구 채팅 테스트
        if (m_MessageInput.text.Length == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //친구매니저에 전달
                DirectMessageStruct DM_Data = new DirectMessageStruct();
                DM_Data.ChattingText = "TestMessage";
                DM_Data.SenderUser = m_OpponentName;
                DM_Data.ReceiverUser = Managers.Data.GetCurrentUser();

                Managers.Data.m_FriendManager.ReceiveDirectMessage(DM_Data);
            }

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
        if(m_OwnerListBar.gameObject.activeInHierarchy)
            m_OwnerListBar.SetChattingDisable();
    }

    void BellButtonCallback()
    {

    }

    void InitMessageList()
    {
        //지정된 상대방의 이름을 통해 가져온다.
        FriendData OpponentData = Managers.Data.m_FriendManager.GetFriendData(m_OpponentName);

        List<DirectMessageStruct> DMList = OpponentData.DirectMessageList;
        int MessageCount = DMList.Count;

        for (int i = 0; i < MessageCount; ++i) 
        {
            DirectMessageStruct MessageData = DMList[i];
            
            if(Managers.Data.GetCurrentUser() == MessageData.SenderUser)
                AddSendMessage(MessageData.ChattingText);

            else
                ReceiveDirectMessage(MessageData.ChattingText);
        }
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

        //친구매니저에 전달
        DirectMessageStruct DM_Data = new DirectMessageStruct();
        DM_Data.ChattingText = Message;
        DM_Data.SenderUser = Managers.Data.GetCurrentUser();
        DM_Data.ReceiverUser = m_OpponentName;

        Managers.Data.m_FriendManager.SendDirectMessage(DM_Data);

        m_ScrollView.verticalScrollbar.value = 0;

        ++m_MessageCnt;
    }

    //초기화할때 UI에 메세지 추가해주는 기능
    void AddSendMessage(string Message)
    {
        RectTransform ContentRect = m_ScrollView.content;

        GameObject MyChat = Instantiate(m_MyChatBoxPrefab, ContentRect);
        MyChat.GetComponentInChildren<Text>().text = Message;

        ChatBoxArea Area = MyChat.GetComponent<ChatBoxArea>();
        Fit(Area.m_BoxRect);
        Fit(Area.m_AreaRect);
        Fit(ContentRect);

        m_ScrollView.verticalScrollbar.value = 0;
    }

    void Fit(RectTransform Rect)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    }

    bool CheckExistReceiveMessage()
    {
        FriendData Data = Managers.Data.m_FriendManager.GetFriendData(m_OpponentName);

        if (Data.FriendName == null)
        {
            Debug.Log("친구이름이 잘못되었습니다.");
            return false;
        }

        //친구 데이터에 존재하는 메세지 수와 현재 UI에 존재하는 메세지 수가 다를 경우 업데이트 필요
        if (Data.DirectMessageList.Count > m_MessageCnt)
            return true;

        else
            return false;
    }

    void UpdateReceiveMessage()
    {
        FriendData Data = Managers.Data.m_FriendManager.GetFriendData(m_OpponentName);

        while(Data.DirectMessageList.Count != m_MessageCnt)
        {
            string ReceiveChatting = Data.DirectMessageList[m_MessageCnt].ChattingText;
            ReceiveDirectMessage(ReceiveChatting);
            ++m_MessageCnt;
        }
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

        m_ScrollView.verticalScrollbar.value = 0;
    }

    void FitScroll()
    {
        RectTransform ContentRect = m_ScrollView.content;
        Fit(ContentRect);
        m_ScrollView.verticalScrollbar.value = 0;
    }

    
}
