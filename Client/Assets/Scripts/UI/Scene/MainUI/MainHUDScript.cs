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
    private GameObject m_MapNavUIPrefab = null;

    [SerializeField]
    private MapNavUI m_MapNavUI = null;
    private GameObject m_MapNavObj = null;

    [SerializeField]
    private GameObject m_SettingUIPrefab = null;

    [SerializeField]
    private SettingUI m_SettingUI = null;
    private GameObject m_SettingObj = null;

    //Button 관련 멤버
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

        if (m_MapNavObj)
        {
            if (!m_MapNavObj.activeInHierarchy)
            {
                m_MapNavUI = null;
                m_MapUION = false;
            }
        }

        if (m_SettingObj)
        {
            if (!m_SettingObj.activeInHierarchy)
            {
                m_SettingUI = null;
                m_SettingUION = false;
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

    public void LogoutButtonCallback()
    {
    }

    public void MapButtonCallback()
    {

        if (m_MapBtn)
        {
            if (!m_MapUION)
            {
                m_MapUION = true;
                m_MapNavObj = Instantiate(m_MapNavUIPrefab);
                
                m_MapNavUI = m_MapNavObj.GetComponentInChildren<MapNavUI>();
            }

            else
            {
                m_MapUION = false;
                Destroy(m_MapNavObj);
                m_MapNavObj = null;
            }
        }
    }

    public void NoticeButtonCallback()
    {

    }

    public void SoundButtonCallback()
    {

    }
    public void SettingButtonCallback()
    {
        if (m_SettingBtn)
        {
            if (!m_SettingUION)
            {
                m_SettingUION = true;
                m_SettingObj = Instantiate(m_SettingUIPrefab);

                m_SettingUI = m_SettingObj.GetComponentInChildren<SettingUI>();
            }

            else
            {
                m_SettingUION = false;
                Destroy(m_SettingObj);
                m_SettingObj = null;
            }
        }
    }
}
