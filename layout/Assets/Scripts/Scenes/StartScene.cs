using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    [SerializeField]
    private   GameObject m_GuestPrefab = null;
    
    [SerializeField]
    private GameObject m_LoginPrefab = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void GuestButtonCallback()
    {
        GameObject.Instantiate(m_GuestPrefab);
    }
    public void LoginButtonCallback()
    {
        GameObject.Instantiate(m_LoginPrefab);
    }
    public static void LoadNextScene()
    {
        Managers.Scene.SetNextScene("EightPathScene");
    }

}
