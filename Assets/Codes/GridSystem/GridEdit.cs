using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEdit
{
    //坐标点
    public Vector2 Point;
    //世界坐标
    public Vector2 Position;
    //是否有植物
    public int Num;


    public GridEdit(Vector2 point, Vector2 position, int num)
    {
        Point = point;
        Position = position;
        Num = num;
    }
}