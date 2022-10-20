using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignUpButton : MonoBehaviour
{
    [SerializeField]
    GameObject m_SignUpPrefab = null;
    public void SignUpCallback()
    {
        GameObject popup = GameObject.Instantiate<GameObject>(m_SignUpPrefab);
    }
}
