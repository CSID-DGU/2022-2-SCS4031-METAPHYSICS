using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingBarWidget : MonoBehaviour
{
    [SerializeField]
    private Image m_PlaceImage;

    [SerializeField]
    private Text m_PlaceText;
    int m_Place = 0;

    [SerializeField]
    private Text m_NameText;

    [SerializeField]
    private Text m_ScoreText;

    [SerializeField]
    private Text m_ScoreNumText;
    int m_Score = 0;

    [SerializeField]
    private Sprite m_FirstPlaceImage;

    [SerializeField]
    private Sprite m_SecondPlaceImage;

    [SerializeField]
    private Sprite m_ThirdPlaceImage;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScoreNumber(int Num)
    {
        m_Score = Num;
        m_ScoreNumText.text = Num.ToString();
    }

    public void SetUserText(string UserName)
    {
        m_NameText.text = UserName;
    }

    public void SetPlaceNumber(int PlaceNum)
    {
        m_Place = PlaceNum;
        m_PlaceText.text = PlaceNum.ToString();

        if(PlaceNum <= 3)
        {
            m_PlaceText.color = Color.clear;

            switch (PlaceNum)
            {
                case 1:
                    m_PlaceImage.sprite = m_FirstPlaceImage;
                    break;
                case 2:
                    m_PlaceImage.sprite = m_SecondPlaceImage;
                    break;
                case 3:
                    m_PlaceImage.sprite = m_ThirdPlaceImage;
                    break;
            }
        }

        else
        {
            m_PlaceImage.color = Color.clear;
        }
    }

    
}
