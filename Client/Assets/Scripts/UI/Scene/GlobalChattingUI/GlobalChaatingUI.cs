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

    [SerializeField]
    private RectTransform m_ImagePanelRect = null;

    [SerializeField]
    private Button m_ResizeButton = null;

    [SerializeField]
    private bool m_IsMinimalized = false;

    [SerializeField]
    private Sprite m_ArrowUpImg = null;
    [SerializeField]
    private Sprite m_ArrowDownImg = null;

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
                //Data.UserName = "박용준";
                Data.UserName = Managers.Data.GetCurrentUser();
                Data.ChattingText = m_MessageInput.text;

                Managers.Chat.SendGlobalChat(Data);

                m_MessageInput.text = null;
            }
        }

    }

    //메세지 받아서 unity에 띄우기
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

    public void SizeButtonCallback()
    {
        if(!m_IsMinimalized)
        {
            Vector2 PanelSize = m_ImagePanelRect.sizeDelta;
            PanelSize.y /= 3.0f;

            m_ImagePanelRect.sizeDelta = PanelSize;

            m_IsMinimalized = true;

            m_ResizeButton.image.sprite = m_ArrowUpImg;
        }

        else
        {

            Vector2 PanelSize = m_ImagePanelRect.sizeDelta;
            PanelSize.y *= 3.0f;

            m_ImagePanelRect.sizeDelta = PanelSize;

            m_IsMinimalized = false;

            m_ResizeButton.image.sprite = m_ArrowDownImg;
        }
    }
    void Fit(RectTransform Rect)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
    }
}
