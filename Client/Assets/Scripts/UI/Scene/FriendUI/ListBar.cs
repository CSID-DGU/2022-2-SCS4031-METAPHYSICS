using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Struct;
using static Define;

public class ListBar : MonoBehaviour
{
    public GameObject m_ChattingWidgetPrefab;
    public GameObject m_VideoChatPrefab;

    public string m_FriendName = null;
    public UserData m_FriendData;
    public bool m_bNewMessage = false;

    public Text m_FriendNameText = null;

    public bool             m_IsONChattingUI = false;
    private DirectChatUI    m_ChattingUI = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void SetUserData(UserData Data)
    {
        m_FriendData = Data;
        m_FriendName = Data.UserName;

        m_FriendNameText.text = m_FriendName;
    }


    public void SetFriendName(string Name)
    {

        m_FriendName = Name;

        m_FriendNameText.text = Name;
    }

    public void ChattingButtonCallback()
    {
        if (m_IsONChattingUI)
            return;

        m_IsONChattingUI = true;

        GameObject ChattingUI = Instantiate(m_ChattingWidgetPrefab);

        m_ChattingUI = ChattingUI.GetComponent<DirectChatUI>();
        m_ChattingUI.SetOpponentName(m_FriendName);
        m_ChattingUI.SetOwnerListBar(this);
    }

    public void VideoChatButtonCallback()
    {
        GameObject VideoObj = Instantiate(m_VideoChatPrefab);

        VideoChatUI VChatUI = VideoObj.GetComponent<VideoChatUI>();

        VChatUI.SetOpponentName(m_FriendName);

    }

    public void SetChattingDisable()
    {
        if (m_IsONChattingUI)
        {
            m_IsONChattingUI = false;
            m_ChattingUI = null;
        }

    }

}
