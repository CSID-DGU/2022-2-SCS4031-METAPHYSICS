using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavInfoManager
{
    public List<NavInfo> OpenList;
    public List<NavInfo> UseList;
    public int OpenCount;
    public int UseCount;

    public NavInfoManager()
    {
        OpenList = new List<NavInfo>();
        UseList = new List<NavInfo>();
        OpenCount = 0;
        UseCount = 0;
    }

}
