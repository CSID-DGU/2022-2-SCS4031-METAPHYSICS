using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;

public class GlobalChatBox : MonoBehaviour
{
    public RectTransform m_BoxRect;
    public RectTransform m_UserNameRect;
    public RectTransform m_TextRect;

    public Text m_UserNameText = null;
    public Text m_ChattingText = null;


    void Start()
    {
        
    }

    public void SetChattingData(string UserName, string ChattingText)
    {
        m_UserNameText.text = UserName;
        m_ChattingText.text = ChattingText;
    }

    public void SetChattingData(GlobalChatData Data)
    {
        m_UserNameText.text = Data.UserName + " : ";
        m_ChattingText.text = " " + Data.ChattingText;
    }

}
