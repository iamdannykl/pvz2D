using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabage : CardTM
{
    // Start is called before the first frame update
    public Transform shootPoint;
    void Start()
    {
        sunCost = 100;
    }


    protected override void XingWei()
    {
        hp = 300f;
        InvokeRepeating("Fashe", 0f, 0.5f);
    }

    public void shootIt()
    {
        cabageBullet cabageBullet = PoolManager.Instance.GetObject(BossManager.Instance.GameConf.cabageBullet).GetComponent<cabageBullet>();
        cabageBullet.transform.position = shootPoint.position;
        cabageBullet.Find();
    }

    void Fashe()//射出豌豆
    {//Debug.Log(FrontGrids);
        if (base.isZombieFront(grdPos, 9) != null)
        {
            if (base.isZombieFront(grdPos, 9).Zombie)
            {
                anim.SetBool("isAtk", true);
                //Instantiate(BossManager.Instance.GameConf.pea, transform.position+new Vector3(0.2f,0.2f,0), quaternion.identity);
            }
            else
            {
                anim.SetBool("isAtk", false);
            }
        }
        else
        {
            anim.SetBool("isAtk", false);
        }
    }
}
