using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;

public class DPG : CardTM
{
    GridS jiaoxiaG;
    GridS FrontGrids;
    public Vector3 pyzd;
    void Start()
    {
        sunCost = 100;
    }


    protected override void XingWei()
    {
        hp = 300f;
        InvokeRepeating("Fashe", 1f, 1f);
    }
    private GridS isZombieFront()
    {
        jiaoxiaG = GridManager.Instance.jiaoxiaGrid(transform.position);//返回植物脚下的Grid
        int a = (int)(jiaoxiaG.Point.x);
        for (int leftzpoint = a; leftzpoint <= a + 4; leftzpoint++)
        {
            FrontGrids = GridManager.Instance.returnGridByPoint(new Vector2(leftzpoint, jiaoxiaG.Point.y));
            //Debug.Log(FrontGrids);
            if (FrontGrids == null) return null;
            if (FrontGrids.Zombie)
            {
                return FrontGrids;
            }
        }
        return null;
    }
    public void penSHe()
    {
        attackEn.Play();
        Instantiate<GameObject>(BossManager.Instance.GameConf.penDan, transform.position + pyzd, quaternion.identity);
    }
    void Fashe()//射出豌豆
    {//Debug.Log(FrontGrids);
        if (isZombieFront() != null)
        {
            if (isZombieFront().Zombie)
            {
                //attackEn.Play();
                //Instantiate(BossManager.Instance.GameConf.pea, transform.position+new Vector3(0.2f,0.2f,0), quaternion.identity);
                anim.SetBool("isPen", true);
            }
            else
            {
                anim.SetBool("isPen", false);
            }
        }
        else
        {
            anim.SetBool("isPen", false);
        }
    }
}
