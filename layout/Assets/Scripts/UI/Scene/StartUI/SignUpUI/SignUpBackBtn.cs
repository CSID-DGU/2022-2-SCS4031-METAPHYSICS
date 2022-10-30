using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpBackBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackBtnCallback()
    {
        Destroy(GetComponentInParent<Canvas>().gameObject);
    }
}
