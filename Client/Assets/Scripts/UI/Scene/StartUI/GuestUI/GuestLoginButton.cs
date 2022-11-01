using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static StartScene;
using static Define;

public class GuestLoginButton : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PopUpUIPrefab = null;

    public void LoginClick()
    {
        if (GameObject.Find("AlertPopUp(Clone)") != null || GameObject.Find("AlertPopUp"))
            return;

        GameObject UserNameObj = GameObject.Find("UserNameField");

        InputField UserNameField = UserNameObj.GetComponent<InputField>();
        string UserName = UserNameField.text;

      

        if (UserName.Length == 0)
        {
            GameObject RejectPrefab = GameObject.Instantiate(m_PopUpUIPrefab);
            Text WarnText = RejectPrefab.GetComponentInChildren<Text>();

            WarnText.text += "이름을 입력해주세요\n";
        }

        else
        {
            Managers.Data.SetCurrentPrivilege(UserPrivileges.Guest);
            Managers.Data.SetCurrentUser(UserName);

            StartScene.LoadNextScene();
        }
    }
}
