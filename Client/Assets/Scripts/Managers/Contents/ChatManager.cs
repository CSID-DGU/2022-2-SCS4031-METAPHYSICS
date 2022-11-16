using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;
using static Define;


public class ChatManager
{
    protected bool _updated = false; //패킷 업데이트

    [SerializeField]
    private GameObject m_GlobalChatUIPrefab;

    private GlobalChaatingUI m_GlobalChatUI;

    private Scene m_CurrentScene;

    Queue<GlobalChatData> m_GlobalChatQueue = new Queue<GlobalChatData>();

    ChatInfo _chatInfo = new ChatInfo();

    public ChatInfo Chat_Info
    {
        get { return _chatInfo; }
        set
        {
            if (_chatInfo.Equals(value))
                return;

            _chatInfo = value;
            UserName = value.UserName;
            ChattingText = value.ChattingText;
        }
    }
    [SerializeField]
    public string UserName //이거 서버 전달
    {
        get
        {
            return Chat_Info.UserName;
        }

        set
        {
            Chat_Info.UserName = value;
            _updated = true;
        }
    }

    [SerializeField]
    public string ChattingText //이거 서버 전달
    {
        get
        {
            return Chat_Info.ChattingText;
        }

        set
        {
            Chat_Info.ChattingText = value;
            _updated = true;
        }
    }

    public void Init()
    {
        m_GlobalChatUIPrefab = Resources.Load<GameObject>("Prefabs\\UI\\GlobalChat\\GlobalChattingUI");
        m_GlobalChatQueue.Clear();
    }

    public void update()
    {
        //여기서 받을 전체채팅 패킷이 있을경우 체크
        //있으면 ReceiveGlobalChat호출

        while(m_GlobalChatQueue.Count > 0)
        {
            GlobalChatData Data = m_GlobalChatQueue.Dequeue();

            ReceiveGlobalChat(Data);
        }
    }

    public void SetGlobalChattingUI(GlobalChaatingUI UI)
    {
        m_GlobalChatUI = UI;
    }

    public void SendGlobalChat(string User_Name, string Message)
    {
        if (m_GlobalChatUI == null)
            return;

        //내 UI에 메세지 띄워주기
        ReceiveGlobalChat(User_Name, Message);

        //패킷만들어서 전송
        //전달
        UserName = User_Name;
        ChattingText = Message;
        CheckUpdatedFlag();
    }

    public void SendGlobalChat(GlobalChatData Data)
    {
        if (m_GlobalChatUI == null)
            return;

        //내 UI에 메세지 띄워주기
        ReceiveGlobalChat(Data);

        //패킷만들어서 전송
        //전달
        UserName = Data.UserName;
        ChattingText = Data.ChattingText;
        CheckUpdatedFlag();
    }

    public void ReceiveGlobalChat(string User_Name, string Message)
    {
        if (m_GlobalChatUI == null)
            return;

        GlobalChatData ChatData;
        ChatData.UserName = User_Name;
        ChatData.ChattingText = Message;

        m_GlobalChatUI.ReceiveMessage(ChatData);
    }

    public void ReceiveGlobalChat(GlobalChatData Data)
    {
        if (m_GlobalChatUI == null)
            return;

        m_GlobalChatUI.ReceiveMessage(Data);
    }

    public void PushGlobalChat(string UserName, string Message)
    {
        GlobalChatData ChatData;
        ChatData.UserName = UserName;
        ChatData.ChattingText = Message;

        m_GlobalChatQueue.Enqueue(ChatData);

    }

    public void PushGlobalChat(GlobalChatData Data)
    {
        m_GlobalChatQueue.Enqueue(Data);

    }

    void CheckUpdatedFlag()
    {
        if (_updated)
        {
            C_Chat chatPacket = new C_Chat();
            chatPacket.ChatInfo = Chat_Info;
            Managers.Network.Send(chatPacket);
            _updated = false;
        }
    }
}
