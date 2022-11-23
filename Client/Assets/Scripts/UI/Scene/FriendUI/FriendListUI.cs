using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;
using UnityEngine.EventSystems;
using UnityEditor;

public class FriendListUI : UI_Drag
{
    [SerializeField]
    private GameObject m_FriendListBarPrefab = null;

    [SerializeField]
    private GameObject m_AlertPopUpPrefab = null;

    [SerializeField]
    private GameObject m_RequestPopUpPrefab = null;

    private InputField m_MessageInput = null;

    private ScrollRect m_ScrollView = null;
    private RectTransform m_ScrollRectTransform = null;

    [SerializeField]
    private InputField m_SearchInputField = null;

    LinkedList<GameObject> m_FriendList;

    // Start is called before the first frame update
    void Start()
    {
        m_ScrollView =  GetComponentInChildren<ScrollRect>();
        m_ScrollRectTransform = m_ScrollView.content;

        InitFriendList();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSearchInputField();
    }

    void InitFriendList()
    {
        List<string> FriendList = Managers.Data.m_FriendManager.GetFriendList();

        for(int i = 0;i<FriendList.Count;++i)
        {
            AddFriend(FriendList[i]);
        }
      
    }

    void AddFriend(string UserName)
    {
        //if(Managers.Data.ContainsUserName(UserName))
        //{
        //    GameObject Obj = Instantiate(m_FriendListBarPrefab, m_ScrollRectTransform);
        //    ListBar Bar = Obj.GetComponent<ListBar>();

        //    Bar.SetFriendName(UserName);
        //}

        //else
        //{
        //    //입력한 정보가 없다는 팝업 생성
        //    GameObject PopUpInstance = Instantiate(m_AlertPopUpPrefab);
        //    Text AlertText = PopUpInstance.GetComponentInChildren<Text>();
        //    AlertText.text = "해당 유저 정보가 존재하지 않습니다.";
        //}

        GameObject Obj = Instantiate(m_FriendListBarPrefab, m_ScrollRectTransform);
        ListBar Bar = Obj.GetComponent<ListBar>();

        Bar.SetFriendName(UserName);
    }

    void FindUser(string UserName)
    {
        if (Managers.Data.m_FriendManager.FindFriend(UserName))
        {
            //입력한 정보가 없다는 팝업 생성
            GameObject PopUpInstance = Instantiate(m_AlertPopUpPrefab);
            Text AlertText = PopUpInstance.GetComponentInChildren<Text>();
            AlertText.text = "이미 등록된 친구입니다.";
        }

        else
        {
            if(Managers.Data.ContainsUserName(UserName))
            {
                //친구 요청 보내기 선택
                GameObject PopUpInstane = Instantiate(m_RequestPopUpPrefab);
                FriendRequestPopUp PopUpUI = PopUpInstane.GetComponent<FriendRequestPopUp>();

                PopUpUI.SetFriendName(UserName);
            }

            else
            {
                //입력한 정보가 없다는 팝업 생성
                GameObject PopUpInstance = Instantiate(m_AlertPopUpPrefab);
                Text AlertText = PopUpInstance.GetComponentInChildren<Text>();
                AlertText.text = "해당 유저 정보가 존재하지 않습니다.";
            }
        }
    }

    void RemoveFriend(string UserName)
    {
    }

    void CheckSearchInputField()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (m_SearchInputField.text.Length == 0)
                m_SearchInputField.ActivateInputField();

            else
            {
                string UserName = m_SearchInputField.text;

                FindUser(UserName);

                m_SearchInputField.text = null;
                m_SearchInputField.DeactivateInputField();
                
            }
        }
    }

    public void SearchButtonCallback()
    {
        if (m_SearchInputField.text.Length != 0)
        { 
            string UserName = m_SearchInputField.text;

            FindUser(UserName);
            m_SearchInputField.text = null;
        }

        m_SearchInputField.DeactivateInputField();
    }
}
