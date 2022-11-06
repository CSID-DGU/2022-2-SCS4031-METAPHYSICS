using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEditor;
using static Define;
using static Struct;

public class GlobalChaatingUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ChattingAreaPrefab = null;

    [SerializeField]
    private InputField m_MessageInput = null;
   
    [SerializeField]
    private ScrollRect m_ScrollView = null;

    private RectTransform m_ScrollRectTransform = null;

    ~GlobalChaatingUI()
    {
        Managers.Chat.SetGlobalChattingUI(null);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_ScrollRectTransform = m_ScrollView.content;

        GlobalChatData data;
        data.ChattingText = "이게뭐지";
        data.UserName = "유재헌";
        Managers.Chat.SetGlobalChattingUI(this);

        ReceiveMessage(data);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (m_MessageInput.text.Length == 0)
                m_MessageInput.ActivateInputField();

            else
            {
                GlobalChatData Data;
                Data.UserName = Managers.Data.GetCurrentUser();
                Data.ChattingText = m_MessageInput.text;

                Managers.Chat.SendGlobalChat(Data);

                m_MessageInput.text = null;
            }
        }

    }

    public void ReceiveMessage(GlobalChatData Data)
    {
        GameObject ChattingObj = Instantiate(m_ChattingAreaPrefab, m_ScrollRectTransform);

        GlobalChatBox BoxArea = ChattingObj.GetComponent<GlobalChatBox>();
        BoxArea.SetChattingData(Data);

        Fit(BoxArea.m_UserNameRect);
        Fit(BoxArea.m_TextRect);
        Fit(BoxArea.m_BoxRect);
        Fit(m_ScrollRectTransform);

        m_ScrollView.verticalScrollbar.value = 0.0f;

    }
    void Fit(RectTransform Rect)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    }
}
