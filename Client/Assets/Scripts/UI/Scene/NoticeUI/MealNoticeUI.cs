using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealNoticeUI : UI_Drag
{
    [SerializeField]
    Text m_MondayMealText = null;

    [SerializeField]
    Text m_TuesdayMealText = null;

    [SerializeField]
    Text m_WednesdayMealText = null;

    [SerializeField]
    Text m_ThursdayMealText = null;

    [SerializeField]
    Text m_FridayMealText = null;

    Text[] m_TextList = new Text[5];

    void Start()
    {
        m_TextList[0] = m_MondayMealText;
        m_TextList[1] = m_TuesdayMealText;
        m_TextList[2] = m_WednesdayMealText;
        m_TextList[3] = m_ThursdayMealText;
        m_TextList[4] = m_FridayMealText;


        string Path = Application.streamingAssetsPath;

        string CrawlingPath1 = Path + "\\Crawling1.txt";
        string CrawlingPath2 = Path + "\\Crawling2.txt";

        string[] CrawlingText = System.IO.File.ReadAllLines(CrawlingPath2);

        int Count = 0;

        for(int i = 0;i< m_TextList.Length;++i)
        {
            string MealText = CrawlingText[Count];

            while(MealText != "")
            {
                m_TextList[i].text += (MealText + "\n");

                ++Count;
                MealText = CrawlingText[Count];
            }

            if (MealText == "")
                ++Count;
        }
    }

    void Update()
    {

    }

    //한줄씩 넘겨받아 처리할 때
    

}
