using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Struct;

public class NavigationManager
{
    [SerializeField]
    Grid m_CurrentGrid = null;

    Navigation m_Navigation = new Navigation();
	Queue<NavData> m_NavMsgQueue = new Queue<NavData>();


    public void Init()
    {
    }

    public void update()
    {
        while(m_NavMsgQueue.Count != 0)
        {
            //NavData
        }
    }
}
