using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridS
{
    //坐标点
    public Vector2 Point;
    //世界坐标
    public Vector2 Position;
    //是否有植物
    public bool Plant;
    public bool Zombie;
    public int Num;
    public bool isHeYe;
    public bool isPlantOnHeYe;
    public bool isIce;
    public gridType gt;

    public List<CardTM> nowCTM = new List<CardTM>();
    /*public CardTM NowCTM
    {
        get => nowCTM;
        set
        {
            nowCTM = value;
            if (nowCTM == null)
                Plant = false;
            else
                Plant = true;
        }
    }*/
    public void setBianliang(bool zombie)
    {
        Zombie = zombie;
    }
    public void setPlant(bool plant)
    {
        Plant = plant;
    }

    public GridS(Vector2 point, Vector2 position, bool plant, bool zombie, int num, gridType grt)
    {
        Point = point;
        Position = position;
        Plant = plant;
        Zombie = zombie;
        Num = num;
        gt = grt;
    }
}
