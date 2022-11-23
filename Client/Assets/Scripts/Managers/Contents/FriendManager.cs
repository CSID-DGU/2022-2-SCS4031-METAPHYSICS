using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager
{
    private List<string> m_FriendList;
    private List<string> m_FriendRequestList;

    public void Init()
    {
        //ģ�� ����Ʈ DB���� �޾ƿͼ� �Ѱ��ֱ�
        m_FriendList = new List<string>();
        InitFriendList();

        //ģ�� ��û �޾ƿͼ� �־������
        m_FriendRequestList = new List<string>();
        InitFriendRequestList();
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

    private void InitFriendRequestList()
    {
        //db���� ģ����û ����Ʈ ���ͼ� �߰�
    }

    public void AddFreind(string FriendName)
    {
        for(int i = 0;i<m_FriendList.Count;++i)
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
        for(int i = 0;i<m_FriendList.Count;++i)
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
