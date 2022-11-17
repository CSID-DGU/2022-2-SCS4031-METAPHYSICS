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
    private GameScene m_NextScene = GameScene.End;

    [SerializeField]
    private Vector3 m_NextInitPos = new Vector3(0.0f, 0.0f, 0.0f);

    private string m_CurrentSceneName = null;

    BoxCollider2D m_BoxCollider = null;
    bool m_OnPlayerCollision = false;
    bool m_PlayerInitCollision = false;
    bool m_IsChangeScene = false;
    

    void Start()
    {
        m_BoxCollider = gameObject.GetComponent<BoxCollider2D>();

        m_CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        Managers.Scene.SetCurrentSceneName(m_CurrentSceneName);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject CollisionObj = collision.gameObject;
        MyPlayerController Controller = CollisionObj.GetComponent<MyPlayerController>();

        if (!Controller)
            return;

        if (m_NextScene == GameScene.End)
        {
            Debug.Log("충돌체에 전환될 씬이 등록되지 않았습니다.");
            m_OnPlayerCollision = false;
            m_SceneChangePopUp = null;
            return;
        }

        if (Managers.Scene.GetPrevSceneName() == m_NextScene.ToString())
        {
            m_PlayerInitCollision = true;
            return;
        }

        m_OnPlayerCollision = true;

        if(Controller.IsAutoMoving())
        {
            GameScene AutoDest = Controller.GetAutoMoveDest();

            if (AutoDest == m_NextScene)
            {
                Managers.Scene.SetNextScene(m_NextScene.ToString());
                Managers.Scene.SetPlayerInitPos(m_NextInitPos);
                m_IsChangeScene = true;
            }

            return;
        }

        if (m_IsChangeScene)
            return;

        m_SceneChangePopUp = Instantiate(m_PopUpPrefab);

        SceneChangePopUp PopUp = m_SceneChangePopUp.GetComponent<SceneChangePopUp>();
        PopUp.SetScene(m_NextScene);
        PopUp.SetPlayerInitPos(m_NextInitPos);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (m_PlayerInitCollision)
        {
            Managers.Scene.SetPrevSceneName(null);
            m_PlayerInitCollision = false;
            return;
        }

        GameObject CollisionObj = collision.gameObject;
        MyPlayerController Controller = CollisionObj.GetComponent<MyPlayerController>();

        if (!Controller)
            return;

        if (Controller.IsAutoMoving() || m_IsChangeScene)
            return;

        m_OnPlayerCollision = false;

        if (m_SceneChangePopUp)
            Destroy(m_SceneChangePopUp);
    }

    public void SetNextInitPos(Vector3 NextInitPos)
    {
        m_NextInitPos = NextInitPos;
    }

    public GameScene GetNextScene()
    {
        return m_NextScene;
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    GameObject CollisionObj = collision.gameObject;

    //    if (!CollisionObj.GetComponent<MyPlayerController>())
    //        return;

    //    if (m_NextScene == GameScene.None)
    //    {
    //        Debug.Log("충돌체에 전환될 씬이 등록되지 않았습니다.");
    //        m_OnPlayerCollision = false;
    //        m_SceneChangePopUp = null;
    //        return;
    //    }

    //    m_OnPlayerCollision = true;

    //    m_SceneChangePopUp = Instantiate(m_PopUpPrefab);

    //    SceneChangePopUp PopUp = m_SceneChangePopUp.GetComponent<SceneChangePopUp>();
    //    PopUp.SetScene(m_NextScene);

    //}
    ////private void OnCollisionStay2D(Collision2D collision)
    ////{
    ////    GameObject CollisionObj = collision.gameObject;

    ////    if (!CollisionObj.GetComponent<MyPlayerController>())
    ////        return;
            
    ////}

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    GameObject CollisionObj = collision.gameObject;

    //    if (!CollisionObj.GetComponent<MyPlayerController>())
    //        return;

    //    m_OnPlayerCollision = false;

    //    if (m_SceneChangePopUp)
    //        Destroy(m_SceneChangePopUp);
    //}
}
