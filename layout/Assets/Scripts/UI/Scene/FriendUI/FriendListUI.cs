using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;
using UnityEngine.EventSystems;
using UnityEditor;

public class FriendListUI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_FriendListBarPrefab = null;

    [SerializeField]
    private Image m_ImagePanel = null;

    private InputField m_MessageInput = null;

    private ScrollRect m_ScrollView = null;
    private RectTransform m_ScrollRectTransform = null;

    LinkedList<GameObject> m_FriendList;

    // Start is called before the first frame update
    void Start()
    {
        m_ScrollView =  GetComponentInChildren<ScrollRect>();
        m_ScrollRectTransform = m_ScrollView.content;

        AddFriend("유재헌");

        GameObject Obj = Instantiate(m_FriendListBarPrefab, m_ScrollRectTransform);
        ListBar Bar = Obj.GetComponent<ListBar>();

        Bar.SetFriendName("Test");

        Obj = Instantiate(m_FriendListBarPrefab, m_ScrollRectTransform);
        Bar = Obj.GetComponent<ListBar>();

        Bar.SetFriendName("Test2");

        Obj = Instantiate(m_FriendListBarPrefab, m_ScrollRectTransform);
        Bar = Obj.GetComponent<ListBar>();

        Bar.SetFriendName("Test3");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddFriend(UserData Data)
    {
        
    }

    void AddFriend(string UserName)
    {
        if(Managers.Data.ContainsUserName(UserName))
        {
            GameObject Obj = Instantiate(m_FriendListBarPrefab, m_ScrollRectTransform);
            ListBar Bar = Obj.GetComponent<ListBar>();

            Bar.SetFriendName(UserName);
        }
    }
}
