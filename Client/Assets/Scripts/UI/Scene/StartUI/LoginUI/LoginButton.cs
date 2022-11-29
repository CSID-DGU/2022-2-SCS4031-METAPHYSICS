using Google.Protobuf.Protocol;
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

    public GameObject m_CustomizeUIPrefab = null;
    public int count = 0;
    public void LoginClick()
    {
        if (GameObject.Find("AlertPopUp(Clone)") != null || GameObject.Find("AlertPopUp")
            || GameObject.Find("LoginAcceptPopUp(Clone)") != null || GameObject.Find("LoginAcceptPopUp"))
            return;

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
            C_LoginCheck loginCheckPacket = new C_LoginCheck();
            loginCheckPacket.AccountId = UserNum;
            Managers.Network.Send(loginCheckPacket);
            count++;

            if(count>1)
            {
                if (!Managers.Data.GetIsPrevUser())
                {
                    WarnText.text = "틀린 학번입니다.";
                }
                else
                {
                    if (Managers.Data.GetUserPassword() == Password)
                    {
                        Destroy(RejectPrefab);

                        Managers.Data.SetCurrentUser(Managers.Data.GetUserName());
                        Managers.Data.SetCurrentPrivilege(UserPrivileges.Student);

                        //커스터마이징 UI 띄워서 설정
                        GameObject CustomizePrefab = GameObject.Instantiate(m_CustomizeUIPrefab);

                        //if (Data.UserColor != Define.UserCustomize.End)
                        //{
                        //    GameObject AcceptPrefab = GameObject.Instantiate(m_AcceptUIPrefab);
                        //    Text AcceptText = AcceptPrefab.GetComponentInChildren<Text>();
                        //    AcceptText.text = Managers.Data.GetUserName() + " 님 환영합니다.";
                        //}

                        //else
                        //{
                        //    //커스터마이징 UI 띄워서 설정
                        //    GameObject CustomizePrefab = GameObject.Instantiate(m_CustomizeUIPrefab);
                        //}
                        count = 0;
                    }

                    else
                    {
                        WarnText.text += "비밀번호를 확인해주세요.";
                    }
                }
            }
        }
    }
}
