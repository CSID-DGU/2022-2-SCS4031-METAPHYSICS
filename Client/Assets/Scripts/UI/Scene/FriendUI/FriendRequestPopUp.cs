using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendRequestPopUp : MonoBehaviour
{
    [SerializeField]
    Text m_Text = null;

    [SerializeField]
    private string m_FriendName = null;

    [SerializeField]
    private GameObject m_AllertPopUpPrefab = null;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFriendName(string FriendName)
    {
        m_Text.text = FriendName;
        m_FriendName = FriendName;
    }

    public void AcceptButtonCallback()
    {
        Managers.Data.m_FriendManager.SendFriendRequest(m_FriendName);

        GameObject PopUp = Instantiate(m_AllertPopUpPrefab);

        Text PopUpText = PopUp.GetComponentInChildren<Text>();
        PopUpText.text = "친구 추가 요청을 보냈습니다.";
    }
}
