using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;
using static Define;


public class ChatManager
{
    [SerializeField]
    private GameObject m_GlobalChatUIPrefab;

    private Scene m_CurrentScene;

    public void Init()
    {
        m_GlobalChatUIPrefab = Resources.Load<GameObject>("Prefabs\\UI\\GlobalChat\\GlobalChattingUI");
    }

    public void update()
    {
    }

    public void SetGlobalChattingUI(GameObject UI)
    {
        ;
    }
}
