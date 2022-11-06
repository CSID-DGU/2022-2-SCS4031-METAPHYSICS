using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;
using static Define;


public class ChatManager
{
    [SerializeField]
    private GameObject m_GlobalChatUIPrefab;

    private GlobalChaatingUI m_GlobalChatUI;

    private Scene m_CurrentScene;

    Queue<GlobalChatData> m_GlobalChatQueue = new Queue<GlobalChatData>();

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

    public void SendGlobalChat(string UserName, string Message)
    {
        if (m_GlobalChatUI == null)
            return;

        //UI에 메세지 띄워주기
        ReceiveGlobalChat(UserName, Message);

        //패킷만들어서 전송하는 기능 추가
    }

    public void SendGlobalChat(GlobalChatData Data)
    {
        if (m_GlobalChatUI == null)
            return;

        //UI에 메세지 띄워주기
        ReceiveGlobalChat(Data);

        //패킷만들어서 전송
    }

    public void ReceiveGlobalChat(string UserName, string Message)
    {
        if (m_GlobalChatUI == null)
            return;

        GlobalChatData ChatData;
        ChatData.UserName = UserName;
        ChatData.ChattingText = Message;

        m_GlobalChatUI.ReceiveMessage(ChatData);
    }

    public void ReceiveGlobalChat(GlobalChatData Data)
    {
        if (m_GlobalChatUI == null)
            return;

        m_GlobalChatUI.ReceiveMessage(Data);
    }

    //패킷받아서 큐에 추가해주는 기능 추가 구현 필요
    //public void PushGlobalChat(PacketMessage Packet)
    //{
    //    GlobalChatData ChatData;
    //    ChatData.UserName = UserName;
    //    ChatData.ChattingText = Message;

    //    m_GlobalChatQueue.Enqueue(ChatData);
    //}

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
}
