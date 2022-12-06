using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizNPCPopUp : UI_Drag
{
    QuizNPC m_OwnerNPC = null;

    [SerializeField]
    GameObject m_QuizPreparePrefab = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetOwnerNPC(QuizNPC Owner)
    {
        m_OwnerNPC = Owner;
    }

    public void AcceptButtonCallback()
    {
        if (m_OwnerNPC.QuizStart)
            return;

        //요청보내기
        m_OwnerNPC.SendStartRequest();
    
    }
}
