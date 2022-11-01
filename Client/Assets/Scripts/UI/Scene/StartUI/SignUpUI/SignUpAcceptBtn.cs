using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Struct;

public class SignUpAcceptBtn : MonoBehaviour
{
    [SerializeField]
    private    GameObject m_PopUpPrefab = null;
    public void SignUpButtonCallback()
    {
        if (GameObject.Find("AlertPopUp(Clone)") != null || GameObject.Find("AlertPopUp"))
            return;

        GameObject UserNumObj = GameObject.Find("UserNumberField_SU");
        GameObject PasswordObj = GameObject.Find("PasswordField_SU");
        GameObject UserNameObj = GameObject.Find("UserNameField_SU");

        InputField UserNumField = UserNumObj.GetComponent<InputField>();
        string UserNum = UserNumField.text;

        InputField PasswordField = PasswordObj.GetComponent<InputField>();
        string Password = PasswordField.text;

        InputField UserNameField = UserNameObj.GetComponent<InputField>();
        string UserName = UserNameField.text;

        GameObject UIPrefab = GameObject.Instantiate(m_PopUpPrefab);
        Text AlertText = UIPrefab.GetComponentInChildren<Text>();

        if (UserNum.Length == 0 || Password.Length == 0 || UserName.Length == 0)
        {
            AlertText.text = "데이터를 모두 입력해주세요.";
        }

        else
        {
            
            bool Overlapped = Managers.Data.IsOverlappedUser(UserNum);

            if(Overlapped)
            {
                AlertText.text = "이미 가입된 학번입니다.";
            }

            else
            {
                UserData UD = new UserData();
                UD.UserNum = UserNum;
                UD.Password = Password;
                UD.UserName = UserName;
                UD.UserColor = Define.UserCustomize.End;

                Managers.Data.AddUserData(UD);

                AlertText.text = UserName + "님 환영합니다.";
                UIPrefab.GetComponent<AllertPopUpBtn>().SetRemoveUIEnable(true, "SignUpUI");
            }
        }

        
    }
}
