using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePicking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit;

            hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.transform != null)
            {
                GameObject HitObj = hit.transform.gameObject;
                Debug.Log(HitObj.name);


                URLOpener OpenerComponent = HitObj.GetComponent<URLOpener>();
                Application.OpenURL(OpenerComponent.URLAdress);
            }

        }
     
        
    }
}
