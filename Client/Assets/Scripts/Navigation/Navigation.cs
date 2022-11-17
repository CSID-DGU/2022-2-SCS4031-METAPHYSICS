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

        //�̵� �Ұ����� ��ġ�� �̵��ϴ� ��� ��ã�� ���
        if (m_NavData.HasTile(GoalInfo.TilePos))
            return false;

        //int count = 0;
        //while (m_NavData.HasTile(GoalInfo.TilePos))
        //{
        //    if (count >= 1000)
        //        Debug.LogError("�ù���¥");

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

        //Ȥ�� �𸣴� �ʱ�ȭ
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

        //�̵��ؾ� �Ѵٸ� true ��ȯ�ϵ��� ��
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


        //���簢�� Ÿ�� ���� �̿���� ����
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
            //�̵��� �� ���ٸ� üũ�� ����
            if (m_NavData.HasTile(Neighbor[i].TilePos))
                continue;

            //�̹� üũ�Ǿ� ������Ͽ� ���ִٸ� ����
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
                //���� ��� ����
                PathList.Clear();

                PathList.Add(GoalPos);

                NavInfo ParentInfo = Node;

                while(ParentInfo != null)
                {
                    PathList.Add(ParentInfo.TilePos);
                    ParentInfo = ParentInfo.ParentInfo;
                }

                //�������� ����
                PathList.RemoveAt(PathList.Count - 1);

                return true;
            }

            //���� �̿����κ��� ���������� �����Ÿ��� ���Ѵ�.
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

            //���� �̿���尡 ���� ��Ͽ� �̹� �� ������
            //���� ���� ���� ������ ����� ���� �� ���� ������� ��ü���ش�.
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

            //���� ��� ��尡 �ƴ϶�� ��� �����Ͽ� ���� ��Ͽ� �־��ش�.
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

        //������� ������ ��ã�Ⱑ �ȳ��� ���̹Ƿ� false ��ȯ
        return false;
    }

    void SetCurrentGrid()
    {
        m_CurrentTileCollider = GameObject.Find("TilemapCollider");

    }
}

