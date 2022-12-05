using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class OXTrigger : MonoBehaviour
{
    [SerializeField]
    private OXMark m_Mark = OXMark.None;

    [SerializeField]
    GameObject m_QuizNPC = null;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //내 플레이어 충돌체가 아닌경우 return 
        if (collision.gameObject.GetComponent<MyPlayerController>() == null)
            return;

        QuizNPC NPC = m_QuizNPC.GetComponent<QuizNPC>();
        NPC.UserAnswer = m_Mark;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //내 플레이어 충돌체가 아닌경우 return 
        if (collision.gameObject.GetComponent<MyPlayerController>() == null)
            return;

        QuizNPC NPC = m_QuizNPC.GetComponent<QuizNPC>();
        NPC.UserAnswer = OXMark.None;

    }
}
