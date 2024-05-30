using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class iceCarZom : ZomPos
{
    public float distanceIce;

    protected override void Update()
    {
        base.Update();
        if (transform.position.x > BossManager.Instance.ys.transform.position.x)
        {
            distanceIce = 0;
        }
        else
        {
            distanceIce = BossManager.Instance.ys.transform.position.x - transform.position.x;
        }
        if (downGrid != null)
        {
            if (!downGrid.Plant)
            {
                eat.Stop();
            }
        }
        //road.GetComponent<iceRoad>().setSize(distanceIce);
        //road.GetComponent<iceRoad>().DisRoad = distanceIce;
        if (BossManager.Instance.GameModeCurrent == BossManager.GameMode.gamer)
            moveZom();
        jiaoxia();
        if (transform.position.x < GridManager.Instance.zuoxia.position.x - GridManager.Instance.XjianGe / 2)
        {
            road.GetComponent<iceRoad>().iceList.Remove(this);
        }
    }
    /*void OnEnable()
    {
        distanceIce = 0;
    }
    void OnDisable()
    {
        distanceIce = 0;
    }*/

}
