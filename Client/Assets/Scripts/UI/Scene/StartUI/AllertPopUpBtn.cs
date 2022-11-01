using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllertPopUpBtn : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private bool m_IsRemoveUI = false;

    string m_OwnerUIName = null;

    public void AllertAccepPopUpCallback()
    {
        if(m_IsRemoveUI && m_OwnerUIName.Length != 0)
        {
            Destroy(GameObject.Find(m_OwnerUIName));
        }
    }

    public void SetRemoveUIEnable(bool Enable, string UIName)
    {
        m_IsRemoveUI = Enable;
        m_OwnerUIName = UIName;
    }
   
}
