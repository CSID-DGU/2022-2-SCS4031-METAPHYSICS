using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class SceneChangePopUp : MonoBehaviour
{

    [SerializeField]
    private GameScene m_NextScene = GameScene.None;

    [SerializeField]
    private Button m_ChangeButton;
    
    [SerializeField]
    private Text m_SceneName;


    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeButtonCallback()
    {
        if (m_NextScene == GameScene.None)
            return;

        string SceneName = m_NextScene.ToString();

        Managers.Scene.SetNextScene(SceneName);
        Destroy(gameObject);
    }

    public void SetScene(GameScene Scene)
    {
        m_NextScene = Scene;

        switch (m_NextScene)
        {
            case GameScene.None:
                return;
            case GameScene.EightPathScene:
                m_SceneName.text = "팔정도";
                break;
            case GameScene.MyeonJinIndoorScene:
                m_SceneName.text = "명진관";
                break;
        }
    }
}
