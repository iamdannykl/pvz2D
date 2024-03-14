using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Ztype
{
    //public GameObject zombieType;
    public string name;
    public int number;

    public float delayTime;

    public float crtSpeed;

    public ZombieType zType;
    public float distanceZ;
    public Ztype(string na, int nu, float dela, float crt, ZombieType zt, float dis)
    {
        name = na;
        number = nu;
        delayTime = dela;
        crtSpeed = crt;
        zType = zt;
        distanceZ = dis;
    }
}
