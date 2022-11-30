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
        //친구 리스트 DB에서 받아와서 넘겨주기
        m_FriendList = new List<string>();
        InitFriendList();

        //친구 요청 받아와서 넣어줘야함
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
        //db에서 친구 리스트 얻어오기

        //테스트용 리스트 생성
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

            //여기서 DirectChatting 패킷 보내야함

        }

    }

    public void ReceiveDirectMessage(DirectMessageStruct ReceiveMessageData)
    {
        string Sender = ReceiveMessageData.SenderUser;
        string Receiver = ReceiveMessageData.ReceiverUser;

        //만약 다른 유저에게 향하는 메세지일경우 무시
        if (Managers.Data.GetCurrentUser() != Receiver)
            return;

        //친구리스트에 등록된 유저로부터의 메세지일 경우 데이터를 복사해 추가
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
            //친구리스트에 없는 유저일경우 더미 데이터 리턴 (받는 입장에서 내용물 확인해야함)
            FriendData DummyData = new FriendData();
            return DummyData;
        }
        
        return m_FriendDataDict[FriendName];
    }

    private void InitFriendRequestList()
    {
        //db에서 친구요청 리스트 얻어와서 추가
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
        //DB에 삭제 요청

        //완료시 클라에서도 삭제

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
        //db통해서 친구요청 보내기

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
