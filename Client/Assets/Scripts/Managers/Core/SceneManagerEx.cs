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
            }
        }
    }

	public void LoadScene(Define.Scene type)
    {
        Managers.Clear();

        SceneManager.LoadScene(GetSceneName(type));

        m_bChangeScene = false;
        m_NextSceneName = null;
    }
    public void LoadScene(string SceneName)
    {
        Managers.Clear();

        SceneManager.LoadScene(SceneName);

        m_bChangeScene = false;
        m_NextSceneName = null;
    }

    public void SetNextScene(string SceneName)
    {
        m_NextSceneName = SceneName;
        m_bChangeScene = true;
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
       // CurrentScene.Clear();
    }
}
