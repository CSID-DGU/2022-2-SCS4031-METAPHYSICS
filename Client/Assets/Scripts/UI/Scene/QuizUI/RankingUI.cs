using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Struct;

public class RankingUI : UI_Drag
{
    QuizRankData[] m_RankData = new QuizRankData[3];

    [SerializeField]
    GameObject m_RankBarPrefab = null;

    [SerializeField]
    RectTransform m_ContentRect = null;

    // Start is called before the first frame update
    void Start()
    {
        InitRankingData();
        CreateRankingBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitRankingData()
    {
        
    }
    void CreateRankingBar()
    {
        for (int i = 0; i < m_RankData.Length; ++i)
        {
            GameObject Obj = Instantiate(m_RankBarPrefab, m_ContentRect);

            RankingBarWidget RankBar = Obj.GetComponentInChildren<RankingBarWidget>();

            RankBar.SetUserText(m_RankData[i].UserName);
            RankBar.SetPlaceNumber(i + 1);
            RankBar.SetScoreNumber(m_RankData[i].CorrectCount);

        }

    }

    public void SetRankData(QuizRankData[] Data)
    {
        m_RankData = Data;
    }
}
