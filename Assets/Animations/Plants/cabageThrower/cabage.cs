using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cabage : CardTM
{
    // Start is called before the first frame update
    public Transform shootPoint;
    public float beilv;
    public float shecheng;
    public float xDanDaoPY;
    Ray2D ray2D;
    RaycastHit2D info;
    void Start()
    {
        sunCost = 100;
    }


    protected override void XingWei()
    {
        hp = 300f;
        InvokeRepeating("FasheSX", 0f, 0.5f);
    }

    public void shootIt()
    {
        if (info.collider != null)
        {
            cabageBullet cabageBullet = PoolManager.Instance.GetObject(BossManager.Instance.GameConf.cabageBullet).GetComponent<cabageBullet>();
            cabageBullet.transform.position = shootPoint.position;
            cabageBullet.xJuLi = info.collider.gameObject.transform.position.x - transform.position.x + xDanDaoPY;
            float hIndex = ((GridManager.Instance.zuoxia.transform.position.x + GridManager.Instance.youshang.transform.position.x) / 2 - GridManager.Instance.zuoxia.transform.position.x - cabageBullet.xJuLi) * beilv;
            cabageBullet.hangGao = nowGrid.Position.y - 0.5f + hIndex;
            cabageBullet.Find();
        }
    }
    void FixedUpdate()
    {
        if (isUpdt)
        {
            ray2D = new Ray2D(transform.position + new Vector3(0, 0.55f, 0), new Vector2(shecheng, 0));
            Debug.DrawRay(ray2D.origin, new Vector2(shecheng, 0), Color.red);
            info = Physics2D.Raycast(ray2D.origin, ray2D.direction);
        }
    }
    void FasheSX()
    {
        if (info.collider != null)
        {
            GameObject obj = info.collider.gameObject;
            if (obj.tag == "zom")
            {
                anim.SetBool("isAtk", true);
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

    void Fashe()//射出cabage
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
