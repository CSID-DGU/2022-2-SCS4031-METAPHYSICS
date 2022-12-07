using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class NoticeUI : UI_Drag
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject m_DetailButtonPrefab = null;

    [SerializeField]
    GameObject m_MealMenuPrefab = null;

    [SerializeField]
    ScrollRect m_ScrollRect = null;

    [SerializeField]
    RectTransform m_RectTransform = null;

    GameObject m_MealMenuObj = null;

    void Start()
    {
        //크롤링 해서 정보 세팅

        string Path = Application.dataPath + "\\Resources\\Data";

        string CrawlingPath1 = Path + "\\Crawling1.txt";
        string CrawlingPath2 = Path + "\\Crawling2.txt";

        string[] CrawlingText = System.IO.File.ReadAllLines(CrawlingPath1);

        for (int i = 0;i< CrawlingText.Length;++i)
        {
            GameObject Obj = Instantiate(m_DetailButtonPrefab, m_ScrollRect.content);

            NoticeListBarUI UI = Obj.GetComponentInChildren<NoticeListBarUI>();
            UI.OwnerRectTransform = m_RectTransform;
            UI.SetText(CrawlingText[i]);
        }
    }

    void Update()
    {
        if (m_MealMenuObj)
        {
            if (!m_MealMenuObj.activeInHierarchy)
                m_MealMenuObj = null;
        }
    }

    public void MealButtonCallback()
    {
        if(m_MealMenuObj == null)
            m_MealMenuObj = Instantiate(m_MealMenuPrefab);
    }
}
