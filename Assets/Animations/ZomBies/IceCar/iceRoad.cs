using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iceRoad : MonoBehaviour
{
    private SpriteRenderer sr;
    public float chixuTime;
    public GridS zuoGrid;
    public int hang;
    public int geshu;
    public List<iceCarZom> iceList = new List<iceCarZom>();
    public float realDis, zuoDis;
    public float RealDis
    {
        get { return realDis; }
        set
        {
            if (value < realDis)
            {
                Invoke(nameof(newZuiDis), chixuTime);
                Debug.Log("disappear!");
            }
            realDis = value;
        }
    }
    public float disRoad;
    public float DisRoad
    {
        get { return disRoad; }
        set
        {
            geshu = (int)(value / GridManager.Instance.XjianGe);
            if (geshu > 8) return;
            disRoad = value;
            sr.size = new Vector2(value, sr.size.y);
            GridS zuoGrid;
            if (geshu > 0)
            {
                for (int i = 0; i <= geshu; i++)
                {
                    zuoGrid = GridManager.Instance.returnGridByPoint(new Vector2(8 - i, 5 - hang));
                    zuoGrid.isIce = true;
                }
            }
            if (geshu == 0)
            {
                for (int i = 0; i < 9; i++)
                {
                    zuoGrid = GridManager.Instance.returnGridByPoint(new Vector2(8 - i, 5 - hang));
                    zuoGrid.isIce = false;
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void setSize(float dis)
    {
        sr.size = new Vector2(dis, sr.size.y);
    }

    void Update()
    {
        if (iceList.Count > 0)
        {
            for (int i = 0; i < iceList.Count; i++)
            {
                if (i < (iceList.Count - 1) && iceList[i + 1].gameObject.transform.position.x < iceList[i].gameObject.transform.position.x)
                {
                    iceCarZom temp = iceList[i];
                    iceList[i] = iceList[i + 1];
                    iceList[i + 1] = temp;
                }
            }
            RealDis = iceList[0].distanceIce;
            if (realDis > zuoDis)
            {
                zuoDis = realDis;
            }

            //iceList[0].road.GetComponent<iceRoad>().DisRoad = iceList[0].distanceIce;

        }
        else
        {
            RealDis = 0;
        }
        //iceList[0].road.GetComponent<iceRoad>().
        DisRoad = zuoDis;
    }
    void newZuiDis()
    {
        zuoDis = realDis;
    }
    // Update is called once per frame
}
