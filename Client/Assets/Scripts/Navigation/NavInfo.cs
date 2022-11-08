using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class NavInfo
{
    public Vector3Int TilePos;
    public float Cost = 0.0f;
    public float Dist = 0.0f;
    public float Total = 0.0f;

    public NavInsert_Type Type = NavInsert_Type.None;
    public NavInfo ParentInfo = null;
}

public class NavInfoComp : IComparer<NavInfo>
{
    public int Compare(NavInfo Src , NavInfo Dest)
    {
        int Compare = Src.Total.CompareTo(Dest.Total);
        return -Compare;
    }
}