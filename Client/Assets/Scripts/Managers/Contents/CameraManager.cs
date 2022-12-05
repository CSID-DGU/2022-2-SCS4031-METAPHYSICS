using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager
{
    bool m_PlayerTrace = true;
    //Default Size 5
    private int m_MainCamOrthographicSize = 5;
    private static int DefaultOrthographicSize = 5;

    public bool CamPlayerTrace
    {
        get
        {
            return m_PlayerTrace;
        }

        set
        {
            m_PlayerTrace = value;
        }
    }

    public int MainCamSize
    {
        get
        {
            return m_MainCamOrthographicSize;
        }

        set
        {
            m_MainCamOrthographicSize = value;
        }
    }

    public void Init()
    {

    }

    public void Update()
    {
        if (m_MainCamOrthographicSize != Camera.main.orthographicSize)
            Camera.main.orthographicSize = m_MainCamOrthographicSize;

    }

    
    public void InitCameraSize()
    {
        Camera.main.orthographicSize = DefaultOrthographicSize;
    }

}
