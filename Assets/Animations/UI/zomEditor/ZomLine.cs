using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomLine
{
    public Vector2 ZomLineLeftPoint;
    public int Hang;
    public List<ZomPos> zomList;
    public ZomLine(Vector2 zomlinepoint, int hang)
    {
        ZomLineLeftPoint = zomlinepoint;
        Hang = hang;
    }
}
