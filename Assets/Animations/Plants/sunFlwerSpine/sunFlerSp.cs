using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

public class sunFlerSp : CardTM
{

    private float wantTime = 1f;
    private bool isShengChan;
    [SpineEvent] public string footstepEventName,sunFns;

    private void Awake()
    {
        sa = GetComponent<SkeletonAnimation>();
        sa.AnimationState.Event += HandleEvent;
    }

    private void HandleEvent(TrackEntry trackentry, Event e)
    {
        if (e.Data.Name.Equals(footstepEventName))
        {
            CreateSun();
        }
        else if(e.Data.Name.Equals(sunFns))
        {
            IsShengChan = false;
        }
    }

    protected override void XingWei()
    {
        hp = 300f;
        InvokeRepeating("startCreateSun", 5, 22);

    }

    public bool IsShengChan
    {
        get => isShengChan;
        set
        {
            if(value==isShengChan)return;
            isShengChan = value;
            if (value)
            {
                sa.state.SetAnimation(0, "sun", true);
            }
            else
            {
                sa.state.SetAnimation(0, "idle", true);
            }
        }
    }
    private void startCreateSun()
    {
        //StartCoroutine(ColorSwitch(wantTime, new Color(1, 0.6f, 0), 0.05f, CreateSun));
        IsShengChan = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hp);
    }
    private void CreateSun()
    {
        sun sun = PoolManager.Instance.GetObject(BossManager.Instance.GameConf.Sun).GetComponent<sun>();
        sun.Find();
        sun.isSunFlowerCrt = true;
        sun.transform.position = transform.position;
        sun.yundong = true;
        sun.cl.color = new Color(1, 1, 1, 1);
        sun.jump();
    }
}

