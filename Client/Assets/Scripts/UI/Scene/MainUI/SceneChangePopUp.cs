using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class SceneChangePopUp : MonoBehaviour
{

    [SerializeField]
    private GameScene m_NextScene = GameScene.End;

    [SerializeField]
    private Button m_ChangeButton;
    
    [SerializeField]
    private Text m_SceneName;

    [SerializeField]
    private Vector3 m_NextInitPos = new Vector3(0.0f, 0.0f, 0.0f);
    public void SetPlayerInitPos(Vector3 InitPos)
    {
        m_NextInitPos = InitPos;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ChangeButtonCallback()
    {
        if (m_NextScene == GameScene.End)
            return;

        string DoorOpenSoundName = "Door_Open_1";
        Managers.Sound.PlayByName(DoorOpenSoundName, Sound.Effect);

        string SceneName = m_NextScene.ToString();

        Managers.Scene.SetNextScene(SceneName);
        Managers.Scene.SetPlayerInitPos(m_NextInitPos);
        Destroy(gameObject);
    }

    public void SetScene(GameScene Scene)
    {
        m_NextScene = Scene;

        switch (m_NextScene)
        {
            case GameScene.End:
                return;
            case GameScene.EightPathScene:
                m_SceneName.text = "팔정도";
                break;
            case GameScene.MyeonJinIndoorScene:
                m_SceneName.text = "명진관";
                break;
            case GameScene.BongwanIndoor:
                m_SceneName.text = "본관";
                break;
            case GameScene.ManhaeOutScene:
                m_SceneName.text = "만해광장";
                break;
            case GameScene.WonHeungIndoor:
                m_SceneName.text = "원흥관";
                break;
            case GameScene.ManhaeGwangjang:
                m_SceneName.text = "미니게임존";
                break;
        }
    }
}
