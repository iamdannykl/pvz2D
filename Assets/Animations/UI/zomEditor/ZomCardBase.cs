using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ZomCardBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private ZomPos zomNow, zomNowInGrid;
    public ZombieType zombieType;
    public float scaleIndex;
    private bool canPlace;
    private bool wantPlace;
    public GameObject zomIsHere;
    private ZomLine currentLine;
    bool canPlc = true;
    public bool CanPlace
    {
        get => canPlace;
        set
        {
            CanPlace = value;
        }
    }
    public bool WantPlace
    {
        get => wantPlace;
        set
        {
            wantPlace = value;
            if (wantPlace)
            {
                canPlace = true;
                GameObject prefab = ZombieTypeManager.Instance.GetZombieFromType(zombieType);
                zomNow = Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, ZombieTypeManager.Instance.transform).GetComponent<ZomPos>();
                zomNow.transform.SetParent(zomIsHere.transform.GetChild(LvManager.Instance.waveNowInEdit));
            }
            else
            {
                if (zomNowInGrid != null)
                {
                    Destroy(zomNowInGrid.gameObject);
                    zomNowInGrid = null;
                }
            }
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (WantPlace && zomNow != null)
        {
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentLine = ZomGrid.Instanse.getLineFromMouse();
            zomNow.transform.position = mousePoint;
            if (canPlace && mousePoint.x >= ZomGrid.Instanse.lineList[1].ZomLineLeftPoint.x && Mathf.Abs(mousePoint.y - currentLine.ZomLineLeftPoint.y) < 0.9f)
            {
                if (zomNowInGrid == null)
                {
                    zomNowInGrid = Instantiate(zomNow, new Vector2(mousePoint.x, currentLine.ZomLineLeftPoint.y), Quaternion.identity);
                    zomNowInGrid.placing();
                }

                else
                {
                    zomNowInGrid.transform.position = new Vector2(mousePoint.x, currentLine.ZomLineLeftPoint.y);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    //                    currentLine.zomList.Add(zomNow);
                    canPlace = false;
                    WantPlace = false;
                    zomNow.sR.sortingOrder = currentLine.Hang;
                    zomNow.placed(currentLine, mousePoint);
                    //zomNow.djg = LvManager.Instance.gqs;
                    zomNow.djb = LvManager.Instance.WaveNowInEdit;
                    zomNow.djh = LvManager.Instance.EditHangShu - currentLine.Hang;
                    //zomNow.crtl = currentLine;
                    //zomNow.ztpp = zombieType;
                    //LvManager.Instance.gq[LvManager.Instance.gqs].waves[LvManager.Instance.WaveNowInEdit].hang[5 - currentLine.Hang].ztp.Add(new Ztype(zomNow.nameZ, 1, Mathf.Abs(zomNow.transform.position.x - currentLine.ZomLineLeftPoint.x) / 0.2760316f, 1, zombieType, Mathf.Abs(zomNow.transform.position.x - currentLine.ZomLineLeftPoint.x)));
                }
            }
            else
            {
                if (zomNowInGrid != null)
                {
                    Destroy(zomNowInGrid.gameObject);
                    zomNowInGrid = null;
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(scaleIndex, scaleIndex, scaleIndex);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!wantPlace)
        {
            BossManager.Instance.CurrentZCard = this;
            WantPlace = true;
            //maskIt.fillAmount = 1;
        }
        else
        {
            WantPlace = false;
            if (zomNow != null)
            {
                Destroy(zomNow.gameObject);
                zomNow = null;
            }
            //maskIt.fillAmount = 0;
        }
    }
}
