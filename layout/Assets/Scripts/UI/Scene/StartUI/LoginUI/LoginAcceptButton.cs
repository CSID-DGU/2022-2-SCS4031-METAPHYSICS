using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginAcceptButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoginAcceptCallback()
    {
        Managers.Scene.SetNextScene("EightPathScene");
    }
}
