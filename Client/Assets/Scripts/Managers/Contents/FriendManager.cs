using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;

public class FriendManager
{
    private List<string> m_FriendList;
    private List<string> m_FriendRequestList;
    private Dictionary<string, FriendData> m_FriendDataDict;

    private bool m_AlarmEnable = true;

    public bool AlarmEnable
    {
        get
        {
            return m_AlarmEnable;
        }

        set
        {
            m_AlarmEnable = value;
        }
    }

    public void Init()
    {
        //ģ�� ����Ʈ DB���� �޾ƿͼ� �Ѱ��ֱ�
        m_FriendList = new List<string>();
        InitFriendList();

        //ģ�� ��û �޾ƿͼ� �־������
        m_FriendRequestList = new List<string>();
        InitFriendRequestList();

        m_FriendDataDict = new Dictionary<string, FriendData>();
        InitFriendData();

    }

    public void Update()
    {

    }

    private void InitFriendList()
    {
        //db���� ģ�� ����Ʈ ������

        //�׽�Ʈ�� ����Ʈ ����
        m_FriendList.Add("Test1");
        m_FriendList.Add("Test2");
        m_FriendList.Add("Test3");
    }

    private void InitFriendData()
    {
        for (int i = 0; i < m_FriendList.Count; ++i)
        {
            FriendData Data = new FriendData();
            Data.FriendName = m_FriendList[i];
            Data.DirectMessageList = new List<DirectMessageStruct>();
            m_FriendDataDict.Add(Data.FriendName, Data);
        }
    }

    public void SendDirectMessage(DirectMessageStruct SendMessageData)
    {
        string Sender = SendMessageData.SenderUser;
        string Receiver = SendMessageData.ReceiverUser;

        if (m_FriendDataDict.ContainsKey(Receiver))
        {
            FriendData Data = m_FriendDataDict[Receiver];

            DirectMessageStruct MessageData = new DirectMessageStruct();
            MessageData.ChattingText = SendMessageData.ChattingText;
            MessageData.SenderUser = SendMessageData.SenderUser;
            MessageData.ReceiverUser = SendMessageData.ReceiverUser;

            Data.DirectMessageList.Add(MessageData);

            //���⼭ DirectChatting ��Ŷ ��������

        }

    }

    public void ReceiveDirectMessage(DirectMessageStruct ReceiveMessageData)
    {
        string Sender = ReceiveMessageData.SenderUser;
        string Receiver = ReceiveMessageData.ReceiverUser;

        //���� �ٸ� �������� ���ϴ� �޼����ϰ�� ����
        if (Managers.Data.GetCurrentUser() != Receiver)
            return;

        //ģ������Ʈ�� ��ϵ� �����κ����� �޼����� ��� �����͸� ������ �߰�
        if(m_FriendDataDict.ContainsKey(Sender))
        {
            FriendData SenderData = m_FriendDataDict[Sender];

            DirectMessageStruct MessageData = new DirectMessageStruct();
            MessageData.ChattingText = ReceiveMessageData.ChattingText;
            MessageData.SenderUser = ReceiveMessageData.SenderUser;
            MessageData.ReceiverUser = ReceiveMessageData.ReceiverUser;

            SenderData.DirectMessageList.Add(MessageData);
        }
    }

    public FriendData GetFriendData(string FriendName)
    {
        if (!m_FriendDataDict.ContainsKey(FriendName))
        {
            //ģ������Ʈ�� ���� �����ϰ�� ���� ������ ���� (�޴� ���忡�� ���빰 Ȯ���ؾ���)
            FriendData DummyData = new FriendData();
            return DummyData;
        }
        
        return m_FriendDataDict[FriendName];
    }

    private void InitFriendRequestList()
    {
        //db���� ģ����û ����Ʈ ���ͼ� �߰�
    }

    public void AddFreind(string FriendName)
    {
        for (int i = 0; i < m_FriendList.Count; ++i)
        {
            if (m_FriendList[i] == FriendName)
                return;
        }

        m_FriendList.Add(FriendName);
    }

    public void RemoveFriend(string FriendName)
    {
        //DB�� ���� ��û

        //�Ϸ�� Ŭ�󿡼��� ����

    }

    public bool FindFriend(string FriendName)
    {
        for (int i = 0; i < m_FriendList.Count; ++i)
        {
            if (m_FriendList[i] == FriendName)
                return true;
        }

        return false;
    }

    public void SendFriendRequest(string FriendName)
    {
        //db���ؼ� ģ����û ������

    }

    public List<string> GetFriendList()
    {
        return m_FriendList;
    }

    public List<string> GetFriendRequestList()
    {
        return m_FriendRequestList;
    }
}
