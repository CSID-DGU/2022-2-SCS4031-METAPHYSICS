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
        m_OwnerNPC.QuizStart = true;
        Managers.Sound.PlayByName("Question_Start", Define.Sound.UIEffect);
        Managers.Cam.CamPlayerTrace = false;
        Managers.Cam.MainCamSize = 6;

        GameObject PrepareObj = Instantiate(m_QuizPreparePrefab);
    
    }
}
