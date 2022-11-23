using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager
{
    private List<string> m_FriendList;
    private List<string> m_FriendRequestList;

    public void Init()
    {
        //친구 리스트 DB에서 받아와서 넘겨주기
        m_FriendList = new List<string>();
        InitFriendList();

        //친구 요청 받아와서 넣어줘야함
        m_FriendRequestList = new List<string>();
        InitFriendRequestList();
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

    private void InitFriendRequestList()
    {
        //db에서 친구요청 리스트 얻어와서 추가
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
        //DB에 삭제 요청

        //완료시 클라에서도 삭제

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
