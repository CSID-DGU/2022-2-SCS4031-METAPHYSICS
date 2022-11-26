using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
    public Image m_FadeImage = null;
    private bool    m_bChangeScene = false;
    private float m_FadeRatio = 0.0f;
    private string  m_NextSceneName = null;
    private string m_CurrentSceneName = null;
    private string m_PrevSceneName = null;
    private bool m_IsInGame = false;

    private Vector3 m_PlayerInitPos = new Vector3(0.0f, 0.0f, 0.0f);

    public void SceneUpdate()
    {
        if (m_bChangeScene && m_NextSceneName != null)
        {
            if (m_FadeRatio <= 1.0f)
            {
                m_FadeRatio += Time.deltaTime * 0.5f;

                if (m_FadeImage == null)
                {
                    Canvas CanvasObj = Component.FindObjectOfType<Canvas>();

                    m_FadeImage = CanvasObj.gameObject.AddComponent<Image>();
                    m_FadeImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

                    m_FadeImage.rectTransform.sizeDelta = new Vector2(1900.0f, 1900.0f);
                    m_FadeImage.rectTransform.anchoredPosition3D = new Vector3(0.0f, 0.0f -9.0f);
               
                }

                m_FadeImage.color = new Color(0.0f, 0.0f, 0.0f, m_FadeRatio);
            }

            else
            {
                this.LoadScene(m_NextSceneName);
                m_PrevSceneName = m_CurrentSceneName;
            }
        }

        if(SceneManager.GetActiveScene().name == m_NextSceneName)
        {
            m_FadeRatio = 0.0f;
            m_NextSceneName = null;
        }
    }

	public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));

        m_bChangeScene = false;
    }
    public void LoadScene(string SceneName)
    {
        Managers.Clear();

        SceneManager.LoadScene(SceneName);

        m_bChangeScene = false;
    }

    public void SetNextScene(string SceneName)
    {
        m_NextSceneName = SceneName;
        m_bChangeScene = true;
        Managers.Data.SetCurrentScene(SceneName);
    }

    public void SetCurrentSceneName(string SceneName)
    {
        m_CurrentSceneName = SceneName;
    }

    public string GetPrevSceneName()
    {
        return m_PrevSceneName;
    }

    public void SetPrevSceneName(string SceneName)
    {
        m_PrevSceneName = SceneName;
    }

    public void SetPlayerInitPos(Vector3 InitPos)
    {
        m_PlayerInitPos = InitPos;
    }

    public Vector3 GetPlayerInitPos()
    {
        return m_PlayerInitPos;
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);

        if (name == "Game")
            m_IsInGame = true;

        else
            m_IsInGame = false;

        m_IsInGame = (name == "Game") ? true : false;

        if (m_IsInGame)
            return name;

        return name;
    }

    public void Clear()
    {
       // CurrentScene.Clear();
    }
}
