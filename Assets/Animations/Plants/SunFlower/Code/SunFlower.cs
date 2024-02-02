using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : CardTM
{
    
    private float wantTime=1f;

    private void Start()
    {
        sunCost = 50;
    }

    protected override void XingWei()
    {
        hp = 300f;
        InvokeRepeating("startCreateSun", 5, 22);
        
    }

    private void startCreateSun()
    {
        StartCoroutine(ColorSwitch(wantTime,new Color(1,0.6f,0),0.05f,CreateSun));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hp);
    }
    private void CreateSun()
    {
        sun sun=PoolManager.Instance.GetObject(BossManager.Instance.GameConf.Sun).GetComponent<sun>();
        sun.Find();
        sun.isSunFlowerCrt = true;
        sun.transform.position = transform.position;
        sun.yundong = true;
        sun.cl.color = new Color(1, 1, 1, 1);
        sun.jump();
    }
}
