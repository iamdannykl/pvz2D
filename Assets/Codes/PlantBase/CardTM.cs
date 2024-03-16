using System;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;
using Spine;
using Event = Spine.Event;
public abstract class CardTM : MonoBehaviour
{
    protected bool isUpdt = false;
    protected GridS jiaoxiaG;//脚下的格子
    protected GridS FrontGrids;//前面的格子
    protected Animator anim;
    protected SpriteRenderer sR;
    protected GridS nowGrid;
    public float hp;
    protected int sunCost;
    protected Vector2 grdPos;
    public float pianYiX, pianYiY;
    public AudioSource hurt1, attackEn;
    protected bool canJiao;
    protected SkeletonAnimation sa;
    public MeshRenderer mr;

    protected virtual GridS isZombieFront(Vector2 jiaoxiaPos, int geShu)
    {
        jiaoxiaG = GridManager.Instance.jiaoxiaGrid(jiaoxiaPos);//返回植物脚下的Grid
        int a = (int)(jiaoxiaG.Point.x);
        for (int leftzpoint = a; leftzpoint < geShu; leftzpoint++)
        {
            FrontGrids = GridManager.Instance.returnGridByPoint(new Vector2(leftzpoint, jiaoxiaG.Point.y));
            //Debug.Log(FrontGrids);
            if (FrontGrids == null) return null;
            if (FrontGrids.Zombie)
            {
                return FrontGrids;
            }
        }
        return null;
    }

    public float Hp
    {
        get => hp;
        set
        {
            hp = value;
        }
    }

    public int SunCost
    {
        get => sunCost;
        set => sunCost = value;
    }

    protected void Find()
    {
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }
    public void sortZom(int sameLineSort, int lineNum)
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
        if (sR != null)
            sR.sortingOrder = LayerNum + sameLineSort;
        else if (sa != null)
        {
            mr.sortingOrder = LayerNum + sameLineSort;
        }
        //        Debug.Log(sR.sortingOrder);
    }
    public void placing(bool isTouMing, GridS grd)//placing
    {
        isUpdt = false;
        Find();
        if (anim != null)
            anim.speed = 0;
        if (sa != null)
            sa.timeScale = 0;
        if (isTouMing)
        {
            if (sR != null)
                sR.color = new Color(1, 1, 1, 0.6f);
            if (sa != null)
            {
                sa.skeleton.A = 0.5f;
            }
            sortZom(50, (int)grd.Point.y);
        }
        else
        {
            if (sR != null)
                sR.sortingOrder = 500;
            else if (sa != null)
            {
                mr.sortingOrder = 500;
            }
        }
    }
    public void placed(GridS grid)//Placed
    {
        isUpdt = true;
        grdPos = grid.Position;
        //sortLayer((int)GridManager.Instance.hpjiaoxiaGrid(grdPos).Point.y);
        //Debug.Log(sR.sortingOrder);
        nowGrid = grid;
        nowGrid.NowCTM = this;
        for (int i = 0; i < 9; i++)
        {
            if (i == nowGrid.Point.x)
            {
                LvManager.Instance.plantSort = -i;
                break;
            }
        }
        sortPlt(LvManager.Instance.plantSort);
        transform.position = grid.Position + new Vector2(pianYiX, pianYiY);
        if (anim != null)
            anim.speed = 1;
        if (sa != null)
            sa.timeScale = 1;
        //sR.sortingOrder = 0;
        XingWei();
        //sR.color=new Color(0.5f, 0.5f, 0.5f);
    }
    public void sortPlt(int sameLineSort)
    {
        int LayerNum = 0;
        switch (nowGrid.Point.y)
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
        if (sR != null)
            sR.sortingOrder = LayerNum + sameLineSort;
        else
        {
            mr.sortingOrder = LayerNum + sameLineSort;
        }
    }
    protected virtual void XingWei()
    {

    }

    public virtual void attacked(float atkValue)
    {
        hp -= atkValue;
        //发光
        canJiao = !canJiao;
        if (canJiao && hurt1 != null)
            hurt1.Play();
        if (sR != null)
            StartCoroutine(ColorSwitch(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        if (hp <= 0)
        {
            //啊我死了，我趋势了
            awsl();
        }
    }

    protected IEnumerator ColorSwitch(float wTime, Color targetColor, float switchTime, UnityAction fun)//被吃的时候闪烁效果
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

    protected void awsl()
    {
        isUpdt = false;
        /*GridManager.Instance.jiaoxiaGrid(grdPos)*/
        nowGrid.setPlant(false);
        Destroy(gameObject);
    }
}
