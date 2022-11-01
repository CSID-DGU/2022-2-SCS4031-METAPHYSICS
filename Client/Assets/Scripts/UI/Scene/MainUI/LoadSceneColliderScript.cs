using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class LoadSceneColliderScript : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PopUpPrefab = null;

    private GameObject m_SceneChangePopUp = null;

    [SerializeField]
    private GameScene m_NextScene = GameScene.None;

    BoxCollider2D m_BoxCollider = null;
    bool m_OnPlayerCollision = false;
    

    void Start()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject CollisionObj = collision.gameObject;

        if (!CollisionObj.GetComponent<MyPlayerController>())
            return;

        if (m_NextScene == GameScene.None)
        {
            Debug.Log("충돌체에 전환될 씬이 등록되지 않았습니다.");
            m_OnPlayerCollision = false;
            m_SceneChangePopUp = null;
            return;
        }

        m_OnPlayerCollision = true;

        m_SceneChangePopUp = Instantiate(m_PopUpPrefab);

        SceneChangePopUp PopUp = m_SceneChangePopUp.GetComponent<SceneChangePopUp>();
        PopUp.SetScene(m_NextScene);

    }
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    GameObject CollisionObj = collision.gameObject;

    //    if (!CollisionObj.GetComponent<MyPlayerController>())
    //        return;
            
    //}

    private void OnCollisionExit2D(Collision2D collision)
    {
        GameObject CollisionObj = collision.gameObject;

        if (!CollisionObj.GetComponent<MyPlayerController>())
            return;

        m_OnPlayerCollision = false;

        if (m_SceneChangePopUp)
            Destroy(m_SceneChangePopUp);
    }
}
