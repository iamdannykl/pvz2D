using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;

public class spineP : CardTM
{
    public Transform shootPoint;
    private bool isAtk;
    //public SpineEventHelper seh;
    [SpineEvent] public string footstepEventName;
    public bool IsAtk
    {
        get => isAtk;
        set
        {
            if (value == isAtk) return;
            isAtk = value;
            if (value)
            {
                sa.state.SetAnimation(0, "attack", true);
            }
            else
            {
                sa.state.SetAnimation(0, "realIdle", true);
            }
        }
    }

    private void Awake()
    {
        sa = GetComponent<SkeletonAnimation>();
        sa.AnimationState.Event += HandleEvent;
    }

    private void HandleEvent(TrackEntry trackentry, Event e)
    {
        if (e.Data.Name.Equals(footstepEventName))
        {
            shootIt();
        }
    }

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
        Debug.Log("dddddddddddd");
        pea pea = PoolManager.Instance.GetObject(BossManager.Instance.GameConf.pea).GetComponent<pea>();
        pea.transform.position = shootPoint.position;
        pea.Find();
    }

    void Fashe()//射出豌豆
    {//Debug.Log(FrontGrids);
        if (base.isZombieFront(grdPos, 9) != null)
        {
            if (base.isZombieFront(grdPos, 9).Zombie)
            {
                IsAtk = true;
                //Instantiate(BossManager.Instance.GameConf.pea, transform.position+new Vector3(0.2f,0.2f,0), quaternion.identity);
            }
            else
            {
                IsAtk = false;
            }
        }
        else
        {
            IsAtk = false;
        }
    }
}
