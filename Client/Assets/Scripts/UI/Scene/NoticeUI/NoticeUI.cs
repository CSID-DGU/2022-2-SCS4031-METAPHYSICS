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
    ScrollRect m_ScrollRect = null;

    [SerializeField]
    RectTransform m_RectTransform = null;

    

    void Start()
    {
        //ũ�Ѹ� �ؼ� ���� ����

        string Path = System.IO.Path.GetDirectoryName(System.Environment.CurrentDirectory);

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
        
    }
}
