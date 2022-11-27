using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class MapNavUI : UI_Drag
{
    [SerializeField]
    private Dropdown m_DropDown = null;


    [SerializeField]
    private int m_SelectIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        m_DropDown.onValueChanged.AddListener(delegate { DropDownCallback(m_DropDown.value); });

        List<string> Options = new List<string>();

        for (int i = 0;i<(int)GameScene.End;++i)
        {
            switch ((GameScene)i)
            {
                case GameScene.EightPathScene:
                    Options.Add("������");
                    break;
                case GameScene.MyeonJinIndoorScene:
                    Options.Add("������");
                    break;
                case GameScene.BongwanIndoor:
                    Options.Add("����");
                    break;
                case GameScene.ManhaeOutScene:
                    Options.Add("���ر���");
                    break;
                case GameScene.WonHeungIndoor:
                    Options.Add("�����");
                    break;
                case GameScene.End:
                    break;
            }
        }

        m_DropDown.AddOptions(Options);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void DropDownCallback(int Option)
    {
        m_SelectIndex = Option;
    }

    public void GoButtonCallback()
    {
        string DestName = ((GameScene)m_SelectIndex).ToString();
        string CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        if (DestName == CurrentSceneName)
        {
            Debug.Log("���� ��ġ�� ���Դϴ�.");
            return;
        }

        else
        {
            Managers.Navigation.FindPath(CurrentSceneName, DestName, true);
        }

    }
}
