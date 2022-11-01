using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;
using static Struct;

public class CustomObjScript : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_CharacterImages = new Sprite[(int)UserCustomize.End];

    [SerializeField]
    private int m_ColorIndex = (int)UserCustomize.Red;

    [SerializeField]
    private GameObject m_LoginAcceptPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        m_ColorIndex = (int)UserCustomize.Red;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColorIndex(int ColorIndex)
    {
        if (ColorIndex < 0)
            m_ColorIndex = (int)UserCustomize.Black;

        else if (ColorIndex >= (int)UserCustomize.End)
            m_ColorIndex = (int)UserCustomize.Red;

        else
            m_ColorIndex = ColorIndex;

        UpdateCharacterColor();
    }

    public void RightButtonCallback()
    {
        SetColorIndex(m_ColorIndex + 1);
    }

    public void LeftButtonCallback()
    {
        SetColorIndex(m_ColorIndex - 1);
    }

    private void UpdateCharacterColor()
    {
        Image[] CharacterImages = GetComponentsInChildren<Image>();
        Image CharacterImage = null;

        for (int i = 0; i < CharacterImages.Length; ++i)
        {
            if (CharacterImages[i].name.Equals("CharacterImage"))
                CharacterImage = CharacterImages[i];
        }

        if (CharacterImage)
            CharacterImage.sprite = m_CharacterImages[m_ColorIndex];

    }

    public void CutomizeAcceptButtonCallback()
    {
        if (GameObject.Find("LoginAcceptPopUp(Clone)") != null || GameObject.Find("LoginAcceptPopUp"))
            return;

        GameObject AcceptObj = GameObject.Instantiate(m_LoginAcceptPrefab);
        Managers.Data.SetCurrentUserColor(m_ColorIndex);

        Text AcceptText = AcceptObj.GetComponentInChildren<Text>();
        AcceptText.text = Managers.Data.GetCurrentUser() + " 님 환영합니다.";
    }
}
