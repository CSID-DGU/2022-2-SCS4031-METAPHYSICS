using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpener : MousePickCallbackObj
{
    [SerializeField]
    private string m_URLAdress = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string URLAdress
    {
        get
        {
            return m_URLAdress;
        }

        set
        {
            m_URLAdress = value;
        }
    }

    public override void MouseClickCallback()
    {
        base.MouseClickCallback();

        if(m_URLAdress!= null)
        {
            Application.OpenURL(m_URLAdress);
        }
    }
}
