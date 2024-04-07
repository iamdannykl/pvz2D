using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shovel : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public static Shovel Instance;
    // Start is called before the first frame update
    public bool wantShovel;
    public Vector2 pianyi;
    public PlantType toolType;
    private GameObject chanZi;
    private GameObject ShuTiao, HengTiao;
    public AudioSource chanzi;
    private bool isOn, isSelected;

    private void Awake()
    {
        Instance = this;
    }

    public bool WantShovel
    {
        get => wantShovel;
        set
        {
            wantShovel = value;
            if (wantShovel == true)
            {
                GameObject prefab = PlantManager.Instance.GetPlantFromType(toolType);
                chanZi = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, PlantManager.Instance.transform);

            }
            else
            {
                if (chanZi != null)
                {
                    Destroy(chanZi.gameObject);
                    chanZi = null;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GridS grid = GridManager.Instance.GetPosByMouse();
        if (wantShovel)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GridS jiaoXia = GridManager.Instance.jiaoxiaGrid(mousePoint);
            chanZi.transform.position = new Vector3(mousePoint.x + pianyi.x, mousePoint.y + pianyi.y, 0);
            if (Vector2.Distance(mousePoint, jiaoXia.Position) < 0.9f)
            {
                Vector2 shu = new Vector2(/*-8.7418f*/GridManager.Instance.shuFirst.x + grid.Point.x * GridManager.Instance.XjianGe, GridManager.Instance.shuFirst.y);
                Vector2 heng = new Vector2(GridManager.Instance.hengFirst.x + 0.3f, GridManager.Instance.hengFirst.y + grid.Point.y * GridManager.Instance.YjianGe);
                if (ShuTiao == null)
                {
                    ShuTiao = Instantiate(BossManager.Instance.GameConf.ShuTiao, shu, quaternion.identity);
                }
                else
                {
                    ShuTiao.transform.position = shu;
                }

                if (HengTiao == null)
                {
                    HengTiao = Instantiate(BossManager.Instance.GameConf.HengTiao, heng, quaternion.identity);
                }
                else
                {
                    HengTiao.transform.position = heng;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    if (jiaoXia.Plant)
                    {
                        WantShovel = false;
                        if (HengTiao != null)
                        {
                            Destroy(HengTiao.gameObject);
                            HengTiao = null;
                        }
                        if (ShuTiao != null)
                        {
                            Destroy(ShuTiao.gameObject);
                            ShuTiao = null;
                        }
                        if (jiaoXia.nowCTM[0].isHY && jiaoXia.nowCTM.Count == 2)
                        {
                            jiaoXia.isPlantOnHeYe = false;
                        }
                        Destroy(jiaoXia.nowCTM[jiaoXia.nowCTM.Count - 1].gameObject);
                        jiaoXia.nowCTM.Remove(jiaoXia.nowCTM[jiaoXia.nowCTM.Count - 1]);
                        if (jiaoXia.nowCTM.Count <= 0)
                            jiaoXia.setPlant(false);
                    }
                    else
                    {
                        WantShovel = false;
                        if (HengTiao != null)
                        {
                            Destroy(HengTiao.gameObject);
                            HengTiao = null;
                        }
                        if (ShuTiao != null)
                        {
                            Destroy(ShuTiao.gameObject);
                            ShuTiao = null;
                        }
                    }
                }
            }
            else
            {
                if (HengTiao != null)
                {
                    Destroy(HengTiao.gameObject);
                    HengTiao = null;
                }
                if (ShuTiao != null)
                {
                    Destroy(ShuTiao.gameObject);
                    ShuTiao = null;
                }
            }
        }

    }/*
    //鼠标移入效果
    public void OnPointerEnter(PointerEventData evenData)
    {
        transform.localScale = new Vector2(2.8f, 2.8f);
    }
    //鼠标移出效果
    public void OnPointerExit(PointerEventData evenData)
    {
        transform.localScale = new Vector2(2.32309f, 2.32309f);
    }*/
    /// <summary>
    /// 
    /// </summary>
    /// <param name="evenData"></param>
    public void OnPointerEnter(PointerEventData evenData)
    {
        isOn = true;
        transform.localScale = new Vector2(1.3f, 1.3f);
    }
    //鼠标移出效果
    public void OnPointerExit(PointerEventData evenData)
    {
        isOn = false;
        transform.localScale = new Vector2(1f, 1f);
        isSelected = false;
    }
    //按下

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!wantShovel)
        {
            UIPlantCards currentCard = UImanager.Instance.CurrentCard;
            if (currentCard != null)
            {
                currentCard.WantPlace = false;
                if (currentCard.HengTiao != null || currentCard.ShuTiao != null)
                {
                    Destroy(currentCard.HengTiao);
                    Destroy(currentCard.ShuTiao);
                }
            }
            chanzi.Play();
            WantShovel = true;
            //maskIt.fillAmount = 1;
        }
        else
        {
            chanzi.Play();
            WantShovel = false;
            if (HengTiao != null)
            {
                Destroy(HengTiao.gameObject);
                HengTiao = null;
            }
            if (ShuTiao != null)
            {
                Destroy(ShuTiao.gameObject);
                ShuTiao = null;
            }
            //maskIt.fillAmount = 0;
        }
    }

    public void desTiao()
    {
        if (HengTiao != null)
        {
            Destroy(HengTiao.gameObject);
            HengTiao = null;
        }
        if (ShuTiao != null)
        {
            Destroy(ShuTiao.gameObject);
            ShuTiao = null;
        }
    }
}
