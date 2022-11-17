using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;
using static Struct;

public class Navigation
{
    GameObject m_CurretnTile;
    GameObject m_CurrentTileCollider;
    Tilemap m_BackGroundTile;
    Tilemap m_NavData;
    static float m_DiagonalDist = Mathf.Sqrt(2);

    NavInfoManager m_NavInfoManager;

    public bool FindPath(Vector3 Start, Vector3 Goal, ref List<Vector3> PathList)
	{
        m_CurretnTile = GameObject.Find("BackGround");
        m_CurrentTileCollider = GameObject.Find("TilemapCollider");
        Grid grid = GameObject.Find("Map").GetComponent<Grid>();
        m_BackGroundTile = m_CurretnTile.GetComponent<Tilemap>();
        m_NavData = m_CurrentTileCollider.GetComponent<Tilemap>();

        NavInfo StartInfo = new NavInfo();
        StartInfo.TilePos = grid.WorldToCell(Start);

        NavInfo GoalInfo = new NavInfo();
        GoalInfo.TilePos = grid.WorldToCell(Goal);


        if (StartInfo.TilePos == GoalInfo.TilePos)
        {
            PathList.Add(Goal);
            return true;
        }

        //이동 불가능한 위치를 이동하는 경우 길찾기 취소
        if (m_NavData.HasTile(GoalInfo.TilePos))
            return false;

        //int count = 0;
        //while (m_NavData.HasTile(GoalInfo.TilePos))
        //{
        //    if (count >= 1000)
        //        Debug.LogError("시발진짜");

        //    float DirX = Goal.x - Start.x;
        //    float DirY = Goal.y - Start.y;

        //    if (StartInfo.TilePos.x != GoalInfo.TilePos.x)
        //    {
        //        if (DirX > 0.0f)
        //            GoalInfo.TilePos.x -= 1;

        //        else if (DirX < 0.0f)
        //            GoalInfo.TilePos.x += 1;
        //    }

        //    if (DirY > 0.0f)
        //        GoalInfo.TilePos.y -= 1;

        //    else if (DirY < 0.0f)
        //        GoalInfo.TilePos.y += 1;

        //    ++count;
        //}

        NavInfoManager NavInfoMan = new NavInfoManager();

        //혹시 모르니 초기화
        NavInfoMan.OpenCount = 0;
        NavInfoMan.UseCount = 0;

        NavInfoMan.OpenList.Add(StartInfo);
        ++NavInfoMan.OpenCount;

        m_NavInfoManager = NavInfoMan;

        NavInfo Node = null;
        NavInfoComp NavComp = new NavInfoComp();

        while (NavInfoMan.OpenCount > 0)
        {
            //if (NavInfoMan.OpenCount > 10000)
            //{
            //    Debug.LogError("InfiniteLoop A*");
            //    break;
            //}

            --NavInfoMan.OpenCount;
            Node = NavInfoMan.OpenList[NavInfoMan.OpenCount];
            NavInfoMan.OpenList.RemoveAt(NavInfoMan.OpenCount);

            Node.Type = NavInsert_Type.Used;

            if (FindNode(Node, GoalInfo, Goal, ref PathList))
                break;
            
            NavInfoMan.OpenList.Sort(NavComp);
        }

        //이동해야 한다면 true 반환하도록 함
        return (PathList.Count != 0);

    }


    bool FindNode(NavInfo Node, NavInfo GoalNode, Vector3 GoalPos
        ,ref List<Vector3> PathList)
    {

        Vector2Int TileSize = new Vector2Int(1, 1);
        Vector3Int NodePos = Node.TilePos;

        NavInfo[] Neighbor = new NavInfo[(int)Neighbor_Dir.ND_End];

        for(int i = 0; i< (int)Neighbor_Dir.ND_End;++i)
        {
            Neighbor[i] = new NavInfo();
        }


        //직사각형 타일 기준 이웃노드 세팅
        Neighbor[(int)Neighbor_Dir.ND_Top].TilePos          = new Vector3Int(Node.TilePos.x, Node.TilePos.y + 1);
        Neighbor[(int)Neighbor_Dir.ND_RightTop].TilePos     = new Vector3Int(Node.TilePos.x + 1, Node.TilePos.y + 1);
        Neighbor[(int)Neighbor_Dir.ND_Right].TilePos        = new Vector3Int(Node.TilePos.x + 1, Node.TilePos.y);
        Neighbor[(int)Neighbor_Dir.ND_RightBottom].TilePos  = new Vector3Int(Node.TilePos.x + 1, Node.TilePos.y - 1);
        Neighbor[(int)Neighbor_Dir.ND_Bottom].TilePos       = new Vector3Int(Node.TilePos.x, Node.TilePos.y - 1);
        Neighbor[(int)Neighbor_Dir.ND_LeftBottom].TilePos   = new Vector3Int(Node.TilePos.x - 1, Node.TilePos.y - 1);
        Neighbor[(int)Neighbor_Dir.ND_Left].TilePos         = new Vector3Int(Node.TilePos.x - 1, Node.TilePos.y);
        Neighbor[(int)Neighbor_Dir.ND_LeftTop].TilePos      = new Vector3Int(Node.TilePos.x - 1, Node.TilePos.y + 1);

        for( int i = 0;i< (int)Neighbor_Dir.ND_End; ++i)
        {
            //이동할 수 없다면 체크를 제외
            if (m_NavData.HasTile(Neighbor[i].TilePos))
                continue;

            //이미 체크되어 닫힌목록에 들어가있다면 제외
            if (Neighbor[i].Type == NavInsert_Type.Used)
                continue;

            Neighbor_Dir Dir1 = Neighbor_Dir.ND_End;
            Neighbor_Dir Dir2 = Neighbor_Dir.ND_End;

            switch ((Neighbor_Dir)i)
            {
                case Neighbor_Dir.ND_RightTop:
                    Dir1 = Neighbor_Dir.ND_Top;
                    Dir2 = Neighbor_Dir.ND_Right;
                    break;
                case Neighbor_Dir.ND_RightBottom:
                    Dir1 = Neighbor_Dir.ND_Bottom;
                    Dir2 = Neighbor_Dir.ND_Right;
                    break;
                case Neighbor_Dir.ND_LeftBottom:
                    Dir1 = Neighbor_Dir.ND_Bottom;
                    Dir2 = Neighbor_Dir.ND_Left;
                    break;
                case Neighbor_Dir.ND_LeftTop:
                    Dir1 = Neighbor_Dir.ND_Top;
                    Dir2 = Neighbor_Dir.ND_Left;
                    break;
            }


            if(Dir1 != Neighbor_Dir.ND_End || Dir2 != Neighbor_Dir.ND_End )
            {
                if (m_NavData.HasTile(Neighbor[(int)Dir1].TilePos)
                    && m_NavData.HasTile(Neighbor[(int)Dir2].TilePos))
                    continue;

                if (m_NavData.HasTile(Neighbor[(int)Dir1].TilePos))
                    continue;

                if (m_NavData.HasTile(Neighbor[(int)Dir2].TilePos))
                    continue;
            }

            else
            {
                switch ((Neighbor_Dir)i)
                {
                    case Neighbor_Dir.ND_Bottom:
                    case Neighbor_Dir.ND_Top:
                        Dir1 = Neighbor_Dir.ND_Left;
                        Dir2 = Neighbor_Dir.ND_Right;
                        break;
                    case Neighbor_Dir.ND_Left:
                    case Neighbor_Dir.ND_Right:
                        Dir1 = Neighbor_Dir.ND_Top;
                        Dir2 = Neighbor_Dir.ND_Bottom;
                        break;
                }

               

                //if (m_NavData.HasTile(Neighbor[(int)Dir1].TilePos)
                //   && m_NavData.HasTile(Neighbor[(int)Dir2].TilePos))
                //    continue;
            }

            if(Neighbor[i].TilePos == GoalNode.TilePos)
            {
                //기존 경로 제거
                PathList.Clear();

                PathList.Add(GoalPos);

                NavInfo ParentInfo = Node;

                while(ParentInfo != null)
                {
                    PathList.Add(ParentInfo.TilePos);
                    ParentInfo = ParentInfo.ParentInfo;
                }

                //시작지점 제외
                PathList.RemoveAt(PathList.Count - 1);

                return true;
            }

            //현재 이웃노드로부터 도착점까지 직선거리를 구한다.
            Vector2 vDist = GoalPos - Neighbor[i].TilePos;

            float Dist = vDist.x * vDist.x + vDist.y * vDist.y;
            float Cost = 0.0f;

            switch ((Neighbor_Dir)i)
            {
                case Neighbor_Dir.ND_Top:
                case Neighbor_Dir.ND_Bottom:
                case Neighbor_Dir.ND_Right:
                case Neighbor_Dir.ND_Left:
                    Cost = Node.Cost + 1;
                    break;
                case Neighbor_Dir.ND_RightTop:
                case Neighbor_Dir.ND_RightBottom:
                case Neighbor_Dir.ND_LeftTop:
                case Neighbor_Dir.ND_LeftBottom:
                    Cost = Node.Cost + 2.0f;
                    break;
            }

            //현재 이웃노드가 열린 목록에 이미 들어가 있으면
            //기존 비용과 현재 구해준 비용을 비교해 더 작은 비용으로 교체해준다.
            if((Neighbor[i].Type == NavInsert_Type.Open))
            {
                if (Neighbor[i].Cost > Cost)
                {
                    Neighbor[i].ParentInfo = Node;
                    Neighbor[i].Cost = Cost;
                    Neighbor[i].Dist = Dist;
                    Neighbor[i].Total = Cost + Dist;
                }

            }

            //열린 목록 노드가 아니라면 비용 대입하여 열린 목록에 넣어준다.
            else
            {
                m_NavInfoManager.UseList.Add(Neighbor[i]);
                ++m_NavInfoManager.UseCount;

                Neighbor[i].Type = NavInsert_Type.Open;
                Neighbor[i].ParentInfo = Node;
                Neighbor[i].Cost = Cost;
                Neighbor[i].Dist = Dist;
                Neighbor[i].Total = Cost + Dist;

                m_NavInfoManager.OpenList.Add(Neighbor[i]);
                ++m_NavInfoManager.OpenCount;
            }
        }

        //여기까지 왔으면 길찾기가 안끝난 것이므로 false 반환
        return false;
    }

    void SetCurrentGrid()
    {
        m_CurrentTileCollider = GameObject.Find("TilemapCollider");

    }
}

