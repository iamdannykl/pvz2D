using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using Unity.Mathematics;
public class UIPlantCards : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,
IPointerClickHandler,IPointerDownHandler,IPointerUpHandler
{
    public enum PlantState
    {
        CanPlace,
        NotCD,
        NotSun,
        NotAll
    }
    private Image maskIt,self;//遮罩组件
    public float CDTime;//CD时间
    private float currentTimeInCD;//当前冷却时间
    private bool canPlace;//是否可以放置植物
    private bool wantPlace;//是否要放置
    //public float pianyiX,pianyiY;
    public GameObject ShuTiao,HengTiao;
    private bool isSelected;
    private bool isOn;
    private CardTM plant;//植物
    private CardTM plantInGrid;//网格透明预览植物
    public PlantType cardType;
    public int sunCost;
    private PlantState cardState=PlantState.NotAll;
    private bool isEnterLQ;
    private GridS grid;

    public PlantState CardState
    {
        get => cardState;
        set
        {
            if(cardState==value)return;
            cardState = value;
            switch (cardState)
            {
                case PlantState.CanPlace:
                    maskIt.fillAmount = 0;
                    self.color = Color.white;
                    break;
                case PlantState.NotCD:
                    if (!isEnterLQ)
                    {
                        CDEnter();
                        isEnterLQ = true;
                    }
                    
                    //Debug.Log("sdsdsd");
                    self.color = Color.white;
                    break;
                case PlantState.NotSun:
                    maskIt.fillAmount = 0;
                    self.color = new Color(0.75f,0.75f,0.75f);
                    break;
                case PlantState.NotAll:
                    if (!isEnterLQ)
                    {
                        CDEnter();
                        isEnterLQ = true;
                    }
                    self.color = new Color(0.75f,0.75f,0.75f);
                    break;
                default:
                    break;
            }
        }
    }
    public bool CanPlace { get => canPlace;
        set
        {
            canPlace = value;
        }
    }

    public void panDuanState()
    {
        if (canPlace&&BossManager.Instance.SunNum>=sunCost)
        {
            CardState = PlantState.CanPlace;
        }
        else if (!canPlace&&BossManager.Instance.SunNum>=sunCost)
        {
            CardState = PlantState.NotCD;
        }
        else if (canPlace&&BossManager.Instance.SunNum<sunCost)
        {
            CardState = PlantState.NotSun;
        }
        else
        {
            CardState = PlantState.NotAll;
        }
    }
    public bool WantPlace { get => wantPlace;
        set {
            wantPlace = value;
            if (wantPlace)
            {
                GameObject prefab = PlantManager.Instance.GetPlantFromType(cardType);
                plant = Instantiate <GameObject>(prefab,Vector3.zero,Quaternion.identity,PlantManager.Instance.transform).GetComponent<CardTM>();
                plant.placing(false,grid);
            }
            else
            {
                if(plant!=null)
                {
                    Destroy(plant.gameObject);
                    plant = null;
                }
                
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
        maskIt=transform.Find("masking").GetComponent<Image>();
        self = GetComponent<Image>();
        CanPlace = true;
            
            //Debug.Log(BossManager.Instance.SunNum);
        transform.GetChild(0).GetComponent<Text>().text = sunCost.ToString();
        
    }

    void chushiPlant(bool kaishiIsLengQue)
    {
        if (BossManager.Instance.SunNum >= sunCost)
        {
            CanPlace = true;
        }
        else
        {
            CanPlace = false;
        }
    }

//开始冷却
    private void CDEnter()
    {
        maskIt.fillAmount = 1;
        StartCoroutine(CalCD());
    }
    IEnumerator CalCD()//携程计算冷却时间
    {
        float calCD = (1 / CDTime) * 0.1f;
        currentTimeInCD = CDTime;
        while (currentTimeInCD>=0)
        {
            yield return new WaitForSeconds(0.1f);
            maskIt.fillAmount -= calCD;
            currentTimeInCD -= 0.1f;
        }
        CanPlace = true;
        isEnterLQ = false;
    }
    
//鼠标移入效果
    public void OnPointerEnter(PointerEventData evenData)
    {
        isOn=true;
        if (cardState!=PlantState.CanPlace)
            return;
        else
        transform.localScale=new Vector2(1.2f,1.2f);
    }
//鼠标移出效果
    public void OnPointerExit(PointerEventData evenData)
    {
        isOn=false;
        transform.localScale=new Vector2(1f,1f);
        isSelected=false;
    }
    
    public void OnPointerDown(PointerEventData evenData)
    {
        isSelected=true;
        if (!CanPlace||BossManager.Instance.SunNum<sunCost)
            return;
        if (!wantPlace)
        {
            UImanager.Instance.CurrentCard = this;
            WantPlace = true;
            //maskIt.fillAmount = 1;
        }
        else
        {
            WantPlace = false;
            //maskIt.fillAmount = 0;
        }
    }
    public void OnPointerUp(PointerEventData evenData)
    {
        if(isOn&&!isSelected)
        {
            if (!CanPlace||BossManager.Instance.SunNum<sunCost)
                return;
            if (!wantPlace)
            {
                UImanager.Instance.CurrentCard = this;
                WantPlace = true;
                //maskIt.fillAmount = 1;
            }
            else
            {
                WantPlace = false;
                //maskIt.fillAmount = 0;
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        panDuanState();
//        Debug.Log(Input.GetMouseButtonUp(0));
        //panDuanState();
        if (wantPlace&&plant!=null)
        {
            //炮台跟随鼠标
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            grid=GridManager.Instance.GetPosByMouse();
            plant.transform.position = new Vector3(mousePoint.x+plant.pianYiX, mousePoint.y+plant.pianYiY, 0);
            //Debug.Log(grid.Plant==false&&Vector2.Distance(mousePoint,grid.Position)<0.9f);
            //Debug.Log(grid.Plant);
            //在鼠标最近的格子上生成一个半透明的炮台
            if (grid.Plant==false&&Vector2.Distance(mousePoint,grid.Position)<0.9f)
            {
                if (plantInGrid == null)
                {
                    if(ShuTiao==null)
                    {
                        ShuTiao=Instantiate(BossManager.Instance.GameConf.ShuTiao,new Vector2(/*-8.7418f*/GridManager.Instance.shuFirst.x+grid.Point.x*GridManager.Instance.XjianGe,GridManager.Instance.shuFirst.y), quaternion.identity);
                    }
                    if(HengTiao==null)
                    {
                        HengTiao=Instantiate(BossManager.Instance.GameConf.HengTiao,new Vector2(GridManager.Instance.hengFirst.x,GridManager.Instance.hengFirst.y+grid.Point.y*GridManager.Instance.YjianGe), quaternion.identity);
                    }
                    plantInGrid = Instantiate(plant.gameObject, grid.Position+new Vector2(plant.pianYiX,plant.pianYiY), Quaternion.identity, PlantManager.Instance.transform).GetComponent<CardTM>();
                    plantInGrid.placing(true,grid);
                    
                }
                else
                {
                    if (ShuTiao != null)
                    ShuTiao.transform.position = new Vector2(GridManager.Instance.shuFirst.x+grid.Point.x*GridManager.Instance.XjianGe,GridManager.Instance.shuFirst.y);
                    if (HengTiao != null)
                    HengTiao.transform.position = new Vector2(GridManager.Instance.hengFirst.x,GridManager.Instance.hengFirst.y+grid.Point.y*GridManager.Instance.YjianGe);
                    plantInGrid.transform.position = grid.Position+new Vector2(plant.pianYiX,plant.pianYiY);
                    plantInGrid.placing(true,grid);
                }
                
                if(Input.GetMouseButtonUp(0))
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
//                    Debug.Log("sdsdsd");
                    AudioManager.Instance.PlantIt();
                    plant.placed(grid);
                    BossManager.Instance.SunNum-=sunCost;
                    if(plant!=null){plant=null;}
                    Destroy(plantInGrid.gameObject);
                    plantInGrid=null;
                    wantPlace=false;
                    CanPlace=false;
                }
            }
            else
            {
                if (plantInGrid != null)
                {
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                }
            }
        } 
    }
    
    public void OnPointerClick(PointerEventData eventData)
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
        /*if (!CanPlace||BossManager.Instance.SunNum<sunCost)
            return;
        if (!wantPlace)
        {
            UImanager.Instance.CurrentCard = this;
            WantPlace = true;
            //maskIt.fillAmount = 1;
        }
        else
        {
            WantPlace = false;
            //maskIt.fillAmount = 0;
        }*/
    }
}
