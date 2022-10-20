using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;

public class LoginButton : MonoBehaviour
{
    public GameObject m_AcceptUIPrefab = null;
    public GameObject m_RejectUIPrefab = null;
    public void LoginClick()
    {
        GameObject UserNumObj = GameObject.Find("UserNumberField");
        GameObject PasswordObj = GameObject.Find("PasswordField");

        InputField UserNumField = UserNumObj.GetComponent<InputField>();
        string UserNum = UserNumField.text;

        InputField PasswordField = PasswordObj.GetComponent<InputField>();
        string Password = PasswordField.text;
        
        GameObject RejectPrefab = GameObject.Instantiate(m_RejectUIPrefab);
        Text WarnText = RejectPrefab.GetComponentInChildren<Text>();

        if (UserNum.Length == 0)
        {
            WarnText.text += "학번을 입력해주세요\n";
        }
       
        else if (Password.Length == 0)
        {
            WarnText.text += "암호를 입력해주세요\n";
        }

        else
        {
            UserData Data = Managers.Data.FindUserData(UserNum);

            if (!Managers.Data.IsOverlappedUser(UserNum))
                WarnText.text += "틀린 학번입니다.";

            else
            {
                if (Data.Password.Equals(Password))
                {
                    Destroy(WarnText.gameObject);

                    GameObject AcceptPrefab = GameObject.Instantiate(m_AcceptUIPrefab);
                    Text AcceptText = AcceptPrefab.GetComponentInChildren<Text>();
                    AcceptText.text = Data.UserName + " 님 환영합니다.";

                    Managers.Data.SetCurrentUser(Data.UserName);
                    Managers.Data.SetCurrentPrivilege(UserPrivileges.Student);
                }

                else
                {
                    WarnText.text += "비밀번호를 확인해주세요.";
                }
            }
        }
    }
}
