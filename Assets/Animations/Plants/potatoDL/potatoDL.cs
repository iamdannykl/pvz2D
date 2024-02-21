using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class potatoDL : CardTM
{
    // Start is called before the first frame update
    public float waitTime;
    private bool isMaoChu;
    void Start()
    {
        anim.SetInteger("potatoInt", 0);//状态0
    }
    protected override void XingWei()
    {
        Invoke("enterZt1", waitTime);
    }
    void enterZt1()//maochu
    {
        anim.SetInteger("potatoInt", 1);
        isMaoChu = true;
    }
    public void enterZt2()//dishang
    {
        anim.SetInteger("potatoInt", 2);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "zom"&&isMaoChu)
        {
            anim.SetInteger("potatoInt",3);
            other.gameObject.GetComponent<ZomPos>().isBoom = true;
            other.gameObject.GetComponent<ZomPos>().Hp1 = 0;
        }
    }

    public void DesSelf()
    {
        Destroy(gameObject);
    }
}
