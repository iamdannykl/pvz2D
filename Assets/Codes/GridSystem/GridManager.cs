using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum gridType
{
    grass,
    water,
    roof
}
public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public Vector2 hangLie;
    private Vector2 realZX;
    private bool isOut = true;
    private List<Vector2> pointList = new List<Vector2>();
    public List<GridS> gridList = new List<GridS>();

    public GameObject cube;
    public Transform zuoxia, youshang;
    public Text tx;
    public float XjianGe, YjianGe;
    //public GameObject shadow;
    public Vector2 shuFirst, hengFirst;
    public float pianYi;
    public Transform[] tr;
    //public List<GridS> youGrid = new List<GridS>();
    private void Awake()
    {
        Instance = this;
        XjianGe = (youshang.position.x - zuoxia.position.x) / (hangLie.y - 1);
        YjianGe = (youshang.position.y - zuoxia.position.y) / (hangLie.x - 1);
        shuFirst = new Vector2(zuoxia.position.x, (zuoxia.position.y + youshang.position.y) / 2);
        hengFirst = new Vector2((zuoxia.position.x + youshang.position.y) / 2, zuoxia.position.y);
        for (int ax = 0; ax < hangLie.x; ax++)
        {
            tr[ax].position = new Vector2(tr[0].position.x, zuoxia.position.y + ax * YjianGe - pianYi);
        }
        realZX = (Vector2)zuoxia.position - new Vector2(XjianGe / 2, YjianGe / 2);
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateGridBaseGrid();
        for (int i = 0; i < 6; i++)
        {
            BossManager.Instance.iceRoad[i].GetComponent<iceRoad>().hang = i;
            //youGrid[i] = gridList[i * 9 + 8];
        }
    }
    void Update()
    {
        //Debug.Log(GetPointByMouse());
        if (Input.GetMouseButtonUp(1))
            Debug.Log(GetPosByMouse().Zombie);
    }

    //基于脚本的形式创建网格
    private void CreateGridBaseGrid()
    {
        int num = 0;
        for (int j = 0; j < hangLie.x; j++)
        {
            for (int i = 0; i < hangLie.y; i++)
            {
                //网格体的保存
                //gridList.Add(new GridS(new Vector2(i,j),transform.position+new Vector3(1.48f*i,1.75f*j,0),false,false,num));
                if (j > 1 && j < 4)
                {
                    gridList.Add(new GridS(new Vector2(i, j), zuoxia.position + new Vector3(XjianGe * i, YjianGe * j, 0), false, false, num, gridType.water));
                    //if(num%2==0)
                }
                else
                    gridList.Add(new GridS(new Vector2(i, j), zuoxia.position + new Vector3(XjianGe * i, YjianGe * j, 0), false, false, num, gridType.grass));
                //if(num%2==0)
                //Instantiate(shadow, zuoxia.position + new Vector3(XjianGe * i, YjianGe * j, 0), quaternion.identity);
                num++;
            }
        }
        //        Debug.Log(num);
    }
    public Vector2 GetPosPointByMouse()
    {
        return GetPosByMouse().Position;
    }
    public GridS _GetPosByMouse()
    {
        //Vector2 point=new Vector2();
        float dis = 100000;
        GridS grid = null;
        Vector2 clickPos = new Vector2();
        clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int i = 0;
        foreach (GridS pos in gridList)
        {
            if (Vector2.Distance(clickPos, pos.Position) < dis)
            {
                dis = Vector2.Distance(clickPos, pos.Position);
                grid = gridList[i];
            }
            i++;
        }
        return grid;
    }
    public GridS GetGridByPoint(Vector2 point)
    {
        if (point == new Vector2(-1, -1)) return null;
        else
        {
            //nowGrd = gridList[(int)(point.x + 1 + point.y * 9) - 1];
            //GD.Print(nowGrd.Plant);
            return gridList[(int)(point.x + 1 + point.y * 9) - 1];
        }
    }
    public GridS GetGridByVerticalNum(int VerticalNum)
    {
        foreach (GridS pos in gridList)
        {
            if (pos.Point == new Vector2(8, VerticalNum))
            {
                return pos;
            }
        }
        return null;
    }
    public GridS jiaoxiaGrid(Vector2 zomPos)
    {
        float dis = 100000;
        GridS grid = null;
        int i = 0;
        foreach (GridS pos in gridList)
        {
            if (Vector2.Distance(zomPos, pos.Position) < dis)
            {
                dis = Vector2.Distance(zomPos, pos.Position);
                grid = gridList[i];
            }
            i++;
        }
        return grid;
    }
    public Vector2 GetPointByMouse()
    {
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = clickPos - realZX;
        Vector2 gridPoint = new Vector2((int)(distance.x / XjianGe), (int)(distance.y / YjianGe));
        //GD.Print(gridPoint, gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > 4 || gridPoint.Y < 0);
        if (gridPoint.x > 8 || gridPoint.x < 0 || gridPoint.y > hangLie.y - 1 || gridPoint.y < 0)
        {
            isOut = true;
            //GD.Print("NULL" + isOut);
            return new Vector2(-1, -1);
        }
        else
        {
            isOut = false;
            //GD.Print((int)(gridPoint.X + 1 + gridPoint.Y * 9) - 1);
            return gridPoint;
        }
    }
    public GridS GetPosByMouse()
    {
        Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 distance = clickPos - realZX;
        Vector2 gridPoint = new Vector2((int)(distance.x / XjianGe), (int)(distance.y / YjianGe));
        //GD.Print(gridPoint, gridPoint.X > 8 || gridPoint.X < 0 || gridPoint.Y > 4 || gridPoint.Y < 0);
        if (gridPoint.x > 8 || gridPoint.x < 0 || gridPoint.y > hangLie.x - 1 || gridPoint.y < 0)
        {
            isOut = true;
            //GD.Print("NULL" + isOut);
            gridPoint = new Vector2(-1, -1);
            return null;
        }
        else
        {
            isOut = false;
            //Debug.Log(gridPoint);
            //Debug.Log((int)(gridPoint.x + 1 + gridPoint.y * 9) - 1);
            return gridList[(int)(gridPoint.x + 1 + gridPoint.y * 9) - 1];
        }
    }
    public GridS returnGridByPoint(Vector2 point)
    {

        foreach (GridS pos in gridList)
        {
            if (point == pos.Point)
            {
                return pos;
            }

        }
        return null;
    }
    public int returnGridNumByPoint(Vector2 point)
    {
        int i = 0;
        foreach (GridS pos in gridList)
        {
            if (point == pos.Point)
            {
                return i;
            }
            else
            {

            }
            i++;
        }
        return -1;
    }
}
