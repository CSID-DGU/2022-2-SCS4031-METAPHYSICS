using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MousePicking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;

            hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform != null)
            {
                //현재 마우스가 UI와 충돌중이라면 return
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                GameObject HitObj = hit.transform.gameObject;
                Debug.Log(HitObj.name);

                MousePickCallbackObj CallbackComp = HitObj.GetComponent<MousePickCallbackObj>();

                if(CallbackComp != null)
                {
                    CallbackComp.MouseClickCallback();
                }
            }

        }
     
        
    }
}
