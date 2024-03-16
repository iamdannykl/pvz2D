using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public abstract class ZomPos : MonoBehaviour
{
    // Start is called before the first frame update

    public enum State
    {
        idle,
        walk,
        death,
        boomDie,
        disAppear
    }
    //private CardTM plant;
    public bool willDlt;
    bool isFrozen;
    public bool isDong;
    public bool canMv = true;
    public bool IsFrozen
    {
        get => isFrozen;
        set
        {
            isFrozen = value;
            if (isFrozen)
            {
                timeJS.InitTime();
                beFrozened();
                //isDong = true;
            }
            else
            {
                jieDong();
                isDong = false;
            }
        }
    }
    TimeJS timeJS;
    float oriTime;
    float nowTime;
    public float 抗冻指数;
    public float reSpd = -1.38f;
    public float oriSpd = -1.38f;
    protected GridS currentGrid;
    public GridS downGrid;
    public int HpOrigin;
    protected int Hp;
    public float speed;
    protected int i, xb;
    public Animator anim;
    protected bool isAttack;//是否在攻击状态
    public bool isBoom, isEat;
    protected float atkValue = 100f;//僵尸の攻击力(hp/s)
    protected int hang;
    protected int lineNum;
    public SpriteRenderer sR;
    public AudioSource eat;
    protected State currentState;
    protected BoxCollider2D coll2d;
    protected bool isUpdate = true;
    protected HPmanager hPmanager;
    public float realSpd;
    public float delayTime;
    public string nameZ;
    public int djg, djb, djh;
    public ZombieType ztpp;
    protected virtual void Update()
    {
        //Debug.Log("wwwwwwwwww");
        if (timeJS.isFns)
        {
            IsFrozen = false;
        }
        if (i >= 0 && i < 9) { Xb = i; hang = 0; }
        if (i >= 9 && i < 18) { Xb = i - 9; hang = 1; }
        if (i >= 18 && i < 27) { Xb = i - 18; hang = 2; }
        if (i >= 27 && i < 36) { Xb = i - 27; hang = 3; }
        if (i >= 36 && i < 45) { Xb = i - 36; hang = 4; }
        if (i >= 45 && i < 54) { Xb = i - 45; hang = 5; }
    }
    IEnumerator freezeIt()
    {
        sR.material.color = new Color(0.6f, 0.7f, 0.95f);
        anim.speed = 0.5f;
        reSpd = oriSpd / 2;
        Debug.Log(抗冻指数);
        yield return new WaitForSeconds(5f);
        Debug.Log("amsnsdsd");
        sR.material.color = new Color(1f, 1f, 1f);
        anim.speed = 1f;
        reSpd = oriSpd;
    }
    public void placing()
    {
        anim.speed = 0;
    }
    public void placed(ZomLine zl, Vector2 ms)
    {
        Find(zl.Hang);
        canMv = false;
        transform.position = new Vector2(ms.x, zl.ZomLineLeftPoint.y);
    }
    void beFrozened()
    {
        sR.material.color = new Color(0.6f, 0.7f, 0.95f);
        anim.speed = 0.5f;
        reSpd = oriSpd / 2;
    }
    void jieDong()
    {
        sR.material.color = new Color(1f, 1f, 1f);
        anim.speed = 1f;
        reSpd = oriSpd;
    }
    public State CurrentState
    {
        get => currentState;
        set
        {
            currentState = value;
            switch (currentState)
            {
                case State.idle:
                    anim.Play("idle");
                    break;
                case State.walk:
                    anim.Play("zomW1");
                    break;
                case State.death:
                    coll2d.enabled = false;
                    isUpdate = false;
                    if (isAttack)
                    {
                        anim.Play("dieAtk");
                        StopAllCoroutines();
                    }
                    else
                    {
                        anim.Play("died");
                    }
                    break;
                case State.boomDie:
                    StopAllCoroutines();
                    coll2d.enabled = false;
                    isUpdate = false;
                    anim.Play("hui");
                    break;
                case State.disAppear:
                    StopAllCoroutines();
                    coll2d.enabled = false;
                    isUpdate = false;
                    Debug.Log(GetComponentInChildren<self>().zomTyp);
                    Hp1 = 0;
                    PoolManager.Instance.SetInPool(ZombieTypeManager.Instance.GetZombieFromType(GetComponentInChildren<self>().zomTyp), gameObject);
                    break;
            }
        }
    }


    public int Hp1
    {
        get => Hp;
        set
        {
            if (value < Hp)
            {
                if (hPmanager.jieDuanShu > 0)
                {
                    hPmanager.NowJieDuan = hPmanager.JieDuanJianCe(value);
                    hPmanager.anim.SetInteger("hurtLevel", hPmanager.NowJieDuan);
                    //                    Debug.Log(hPmanager.NowJieDuan);
                }

            }
            Hp = value;
            if (Hp <= 0)
            {
                GridManager.Instance.jiaoxiaGrid(transform.position).setBianliang(false);//当前网格变为false
                LvManager.Instance.AllZomNum++;
                if (!isBoom && !isEat)
                    CurrentState = State.death;
                else if (isBoom)
                {
                    CurrentState = State.boomDie;
                }
                else if (isEat)
                {
                    CurrentState = State.disAppear;
                }

            }
        }
    }
    public void
    Find(int line)
    {
        isFrozen = false;
        timeJS = GetComponent<TimeJS>();
        timeJS.timeChiXu = 抗冻指数;
        anim = transform.GetChild(0).GetComponent<Animator>();
        sR = transform.GetChild(0).GetComponent<SpriteRenderer>();
        sR.material.color = new Color(1f, 1f, 1f);
        anim.speed = 1f;
        hPmanager = GetComponent<HPmanager>();
        isBoom = false;
        Hp = HpOrigin;
        i = 100;
        canMv = true;//<===============================================
        isUpdate = true;
        coll2d = GetComponent<BoxCollider2D>();
        coll2d.enabled = true;

        reSpd = oriSpd;
        lineNum = line;
    }
    protected virtual void XingWei()
    {

    }

    private void Awake()
    {
        Find(hang);
    }

    public void sortZom(int sameLineSort)
    {
        int LayerNum = 0;
        switch (lineNum)
        {
            case 5:
                LayerNum = 0;
                break;
            case 4:
                LayerNum = 100;
                break;
            case 3:
                LayerNum = 200;
                break;
            case 2:
                LayerNum = 300;
                break;
            case 1:
                LayerNum = 400;
                break;
            case 0:
                LayerNum = 500;
                break;
            default:
                break;
        }
        sR.sortingOrder = LayerNum + sameLineSort;
    }
    // Update is called once per frame



    public int Xb
    {
        get => xb;
        set
        {
            xb = value;
            int a = 0;
            if (hang == 0) a = xb;
            if (hang == 1) a = xb + 9;
            if (hang == 2) a = xb + 18;
            if (hang == 3) a = xb + 27;
            if (hang == 4) a = xb + 36;
            if (xb < 8)
            {
                GridManager.Instance.gridList[a + 1].setBianliang(false);
            }
        }
    }

    protected void jiaoxia()
    {
        if (Vector2.Distance(transform.position, GridManager.Instance.jiaoxiaGrid(transform.position).Position) < 1.5f)
        {
            downGrid = GridManager.Instance.jiaoxiaGrid(transform.position);
            if (Hp > 0)
                downGrid.setBianliang(true);
            if (downGrid == null)
            {
                i = 100;
            }
            else
            {
                i = downGrid.Num;
                GridManager.Instance.gridList[i] = downGrid;
            }

        }
    }
    void zuoYi()
    {
        if (canMv)
            transform.Translate(new Vector2(reSpd, 0) * Time.deltaTime / speed);
    }
    protected void moveZom()
    {
        //if(currentGrid==null)return;

        if (!isUpdate)
        {
            if (!isBoom && !isAttack)
                zuoYi();
        }
        else if (downGrid == null)
        {
            zuoYi();
        }

        else
        {
            if (downGrid.Plant && transform.position.x - downGrid.Position.x < 0.35f)
            {
                if (!isAttack)
                    attackP(downGrid.NowCTM);
            }
            else
            {
                //StopAllCoroutines();
                isAttack = false;
                anim.SetBool("isEating", false);
                zuoYi();
            }
            //}
        }
    }
    protected void GetGridByVertical(int Vnum)
    {
        currentGrid = GridManager.Instance.GetGridByVerticalNum(Vnum);
        gameObject.transform.position = new Vector3(transform.position.x, currentGrid.Position.y, transform.position.z);
        //Debug.Log(currentGrid.Position);
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "kill")
        {
            isBoom = true;
            Hp1 = 0;
            if (Hp1 <= 0)
            {
                sR.material.color = new Color(1f, 1f, 1f);
                anim.speed = 1f;
            }
            StartCoroutine(ColorSwitch(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        }
    }

    public void ShanLiang()
    {
        StartCoroutine(ColorSwitch(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
    }
    protected void attackP(CardTM plant)
    {
        isAttack = true;
        Debug.Log("chi");
        anim.SetBool("isEating", true);//播放吃植物动画 
        //StartCoroutine(hurtPlant(plant));
    }
    public void eatPlt()
    {
        if (downGrid.NowCTM != null)
        {
            downGrid.NowCTM.attacked(atkValue * 0.65f);
            eat.Play();
        }
    }
    /*IEnumerator hurtPlant(CardTM plant)
    {
        if (downGrid.NowCTM != null)
        {
            while (downGrid.NowCTM != null && downGrid.NowCTM.hp > 0)
            {//Debug.Log("aaaaaa");
                plant.attacked(atkValue * 0.65f);
                eat.Play();
                yield return new WaitForSeconds(0.65f);
            }
        }
    }*/
    IEnumerator ColorSwitch(float wTime, Color targetColor, float switchTime, UnityAction fun)
    {
        float currentTime = 0;
        float lerp;
        while (currentTime < wTime)
        {
            yield return new WaitForSeconds(switchTime);
            lerp = currentTime / wTime;
            currentTime += switchTime;
            sR.color = Color.Lerp(Color.white, targetColor, lerp);
        }

        sR.color = Color.white;
        if (fun != null) fun();
    }
}
