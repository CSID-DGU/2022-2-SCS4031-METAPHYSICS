using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHUDScript : MonoBehaviour
{
    [SerializeField]
    private GameObject m_FriendListUIPrefab = null;

    [SerializeField]
    private FriendListUI m_FriendListUI = null;
    private GameObject m_FriendListObj = null;

    [SerializeField]
    private Button m_FriendBtn = null;
    public bool m_FriendListON = false;

    [SerializeField]
    private Button m_MapBtn = null;
    public bool m_MapUION = false;

    [SerializeField]
    private Button m_NoticeBtn = null;
    public bool m_NoticeUION = false;

    [SerializeField]
    private Button m_SoundBtn = null;
    public bool m_SoundUION = false;

    [SerializeField]
    private Button m_SettingBtn = null;
    public bool m_SettingUION = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (m_FriendListObj)
        {
            if (!m_FriendListObj.activeInHierarchy)
            {
                m_FriendListUI = null;
                m_FriendListON = false;
            }
        }

    }

    public void FriendButtonCallback()
    {
        if (m_FriendBtn)
        {
            if (!m_FriendListON)
            {
                m_FriendListON = true;
                m_FriendListObj = Instantiate(m_FriendListUIPrefab);

                m_FriendListUI = m_FriendListObj.GetComponentInChildren<FriendListUI>();
            }

            else
            {
                m_FriendListON = false;
                Destroy(m_FriendListObj);
                m_FriendListObj = null;
            }
        }
    }

    public void MapButtonCallback()
    {

    }

    public void NoticeButtonCallback()
    {

    }

    public void SoundButtonCallback()
    {

    }
    public void SettingButtonCallback()
    {

    }
}
