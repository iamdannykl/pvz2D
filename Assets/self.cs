using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class self : MonoBehaviour
{
    private Animator anim;
    public ZombieType zomTyp;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void atk()
    {
        transform.parent.GetComponent<ZomPos>().eatPlt();
    }
    public void enterShui()
    {
        transform.parent.GetComponent<ZomPos>().canMv = false;
    }
    public void chongci()
    {
        if (BossManager.Instance.GameModeCurrent == BossManager.GameMode.gamer)
        {
            transform.parent.GetComponent<ZomPos>().canMv = true;
            GetComponent<Animator>().Play("rush");
        }
    }

    public void padi()
    {
        if (zomTyp == ZombieType.haiTunZom)
        {
            transform.parent.GetComponent<ZomPos>().canMv = false;
        }
        anim.Play("padi");
    }
    // Start is called before the first frame update
    public void desz()
    {
        //Destroy(transform.parent);
        PoolManager.Instance.SetInPool(ZombieTypeManager.Instance.GetZombieFromType(zomTyp), gameObject.transform.parent.gameObject);
        if (zomTyp == ZombieType.haiTunZom)
        {
            GetComponent<Animator>().SetBool("isJump", false);
            GetComponent<Animator>().SetBool("isRuShui", false);
        }
    }
    public void RuShui()
    {
        //Debug.Log(transform.parent.GetComponent<ZomPos>().oriSpd);
        transform.parent.GetComponent<ZomPos>().reSpd = transform.parent.GetComponent<ZomPos>().oriSpd / 4f;
        transform.parent.GetComponent<ZomPos>().oriSpd = transform.parent.GetComponent<ZomPos>().oriSpd / 4f;
        //Debug.Log(transform.parent.GetComponent<ZomPos>().reSpd);
        Invoke("EnableCollider", 0.2f);
        GetComponent<Animator>().SetBool("isRuShui", true);
        transform.parent.GetComponent<haiTunZom>().target = null;
        //EnableCollider();
        //transform.parent.GetComponent<haiTunZom>().isTiaoYueZom = false;
    }
    void EnableCollider()
    {
        transform.parent.GetComponent<haiTunZom>().isTiaoYueZom = false;
        transform.parent.GetComponent<BoxCollider2D>().enabled = true;

    }
    public void YueQi()
    {
        transform.parent.GetComponent<BoxCollider2D>().enabled = false;
        transform.parent.GetComponent<ZomPos>().reSpd *= 3;
        transform.parent.GetComponent<ZomPos>().haiTunYueqi.Play();
    }
}
