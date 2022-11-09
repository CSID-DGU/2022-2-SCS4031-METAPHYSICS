using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoChatUI : UI_Drag
{
    public RawImage m_Display;
    public WebCamTexture m_CamTexture;
    private int m_CurrentIndex = 0;

    public string m_OpponentName = null;
    public Text m_OpponentText = null;

    void Start()
    {
        if (m_CamTexture != null)
        {
            m_Display.texture = null;
            m_CamTexture.Stop();
            m_CamTexture = null;
        }

        WebCamDevice Device = WebCamTexture.devices[m_CurrentIndex];
        m_CamTexture = new WebCamTexture(Device.name);
        m_Display.texture = m_CamTexture;
        m_CamTexture.Play();

        m_CamTexture.requestedFPS = 60;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    void OnDestroy()
    {
        if (m_CamTexture)
        {
            m_CamTexture.Stop();
            Destroy(m_CamTexture);
            m_CamTexture = null;
        }
    }
    public void ExitButtonCallback()
    {
        if (m_CamTexture)
        {
            m_CamTexture.Stop();
            Destroy(m_CamTexture);
            m_CamTexture = null;
        }
    }
    public void SetOpponentName(string Name)
    {
        m_OpponentName = Name;

        m_OpponentText.text = Name;
    }
}
