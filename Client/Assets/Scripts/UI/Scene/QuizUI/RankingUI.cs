using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
using static Struct;

public class RankingUI : UI_Drag
{
    List<QuizRankData> m_QuizDataList = new List<QuizRankData>();

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
        for (int i = 0; i < 5; ++i)
        {
            GameObject Obj = Instantiate(m_RankBarPrefab, m_ContentRect);

            RankingBarWidget RankBar = Obj.GetComponentInChildren<RankingBarWidget>();

            RankBar.SetUserText("Test" + (i + 1).ToString());
            RankBar.SetPlaceNumber(i + 1);
            RankBar.SetScoreNumber(5 - i);

        }

    }

    public void SetRankData()
    {

    }
}
