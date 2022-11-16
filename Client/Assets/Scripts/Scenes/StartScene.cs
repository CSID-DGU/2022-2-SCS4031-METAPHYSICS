using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : BaseScene
{
    [SerializeField]
    private   GameObject m_GuestPrefab = null;
    
    [SerializeField]
    private GameObject m_LoginPrefab = null;

    protected override void Init()
    {
        base.Init();

        Screen.SetResolution(1920, 980, false);
        //Screen.SetResolution(640, 480, false);
    }

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

    public override void Clear()
    {

    }
}
