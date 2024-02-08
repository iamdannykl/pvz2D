using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ZomPos : MonoBehaviour//zombieの基类
{
    // Start is called before the first frame update

    public enum State
    {
        idle,
        walk,
        death,
        boomDie
    }
    protected GridS currentGrid;
    public GridS downGrid;
    public int HpOrigin;
    protected int Hp;
    public float speed;
    protected int i, xb;
    protected Animator anim;
    protected bool isAttack, isBoom;//是否在攻击状态
    protected float atkValue = 100f;//僵尸の攻击力(hp/s)
    protected int hang;
    protected int lineNum;
    protected SpriteRenderer sR;
    public AudioSource eat;
    protected State currentState;
    protected BoxCollider2D coll2d;
    protected bool isUpdate = true;
    protected HPmanager hPmanager;
    protected State CurrentState
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
                if (!isBoom)
                    CurrentState = State.death;
                else
                {
                    CurrentState = State.boomDie;
                }

            }
        }
    }
    public void Find(int line)
    {
        hPmanager = GetComponent<HPmanager>();
        isBoom = false;
        Hp = HpOrigin;
        i = 100;
        isUpdate = true;
        coll2d = GetComponent<BoxCollider2D>();
        coll2d.enabled = true;
        anim = transform.GetChild(0).GetComponent<Animator>();
        sR = transform.GetChild(0).GetComponent<SpriteRenderer>();
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
            case 4:
                LayerNum = 0;
                break;
            case 3:
                LayerNum = 100;
                break;
            case 2:
                LayerNum = 200;
                break;
            case 1:
                LayerNum = 300;
                break;
            case 0:
                LayerNum = 400;
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
    protected void moveZom()
    {
        //if(currentGrid==null)return;

        if (!isUpdate)
        {
            if (!isBoom && !isAttack)
                transform.Translate(new Vector2(-1.38f, 0) * (Time.deltaTime / 1) / speed);
        }
        else if (downGrid == null)
        {
            transform.Translate(new Vector2(-1.38f, 0) * (Time.deltaTime / 1) / speed);
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
                isAttack = false;
                anim.SetBool("isEating", false);
                transform.Translate(new Vector2(-1.38f, 0) * (Time.deltaTime / 1) / speed);
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
            StartCoroutine(ColorSwitch(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
            isBoom = true;
            Hp1 = 0;
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
        StartCoroutine(hurtPlant(plant));
    }
    IEnumerator hurtPlant(CardTM plant)
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
    }
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
