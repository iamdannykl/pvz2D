using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ZomGrid : MonoBehaviour
{
    public static ZomGrid Instanse;
    public Transform YS;
    public GameObject dadie;
    float YjianJu;
    public Vector2 hanglie;
    // Start is called before the first frame update
    public List<ZomLine> lineList = new List<ZomLine>();
    private void Awake()
    {
        Instanse = this;
        intiLines();
    }
    private void intiLines()
    {
        YjianJu = (YS.position.y - transform.position.y) / hanglie.x;
        Vector3 YJJ = new Vector3(0, YjianJu, 0);
        Vector3 bottomRedLine = transform.position + YJJ * 0.35f;
        Vector3 upRedLine = YS.transform.position - YJJ * (1 - 0.35f);
        for (int i = 0; i <= hanglie.x - 1; i++)
        {
            //Gizmos.DrawLine(i * YJJ + transform.position, YS.position - YJJ * (5 - i));
            lineList.Add(new ZomLine((i) * YJJ + bottomRedLine, (int)(hanglie.x - i)));
            //Gizmos.DrawLine((i - 1) * YJJ + bottomRedLine, upRedLine - (i - 1) * YJJ);
        }
    }

    // Update is called once per frame
    public ZomLine getLineFromMouse()
    {
        float dis = 100000;
        ZomLine TargetLine = null;
        Vector2 clickPos = new Vector2();
        clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i <= hanglie.x - 1; i++)
        {
            if (clickPos.x >= lineList[1].ZomLineLeftPoint.x && Mathf.Abs(clickPos.y - lineList[i].ZomLineLeftPoint.y) < dis)
            {
                dis = Mathf.Abs(clickPos.y - lineList[i].ZomLineLeftPoint.y);
                TargetLine = lineList[i];
            }
        }
        return TargetLine;
    }
    private void OnDrawGizmos()
    {
        YjianJu = (YS.position.y - transform.position.y) / hanglie.x;
        Vector3 YJJ = new Vector3(0, YjianJu, 0);
        Gizmos.DrawWireCube((transform.position + YS.position) / 2, YS.position - transform.position);
        Vector3 bottomRedLine = transform.position + YJJ * 0.35f;
        Vector3 upRedLine = YS.transform.position - YJJ * (1 - 0.35f);
        for (int i = 1; i <= hanglie.x - 1; i++)
        {
            Gizmos.DrawLine(i * YJJ + transform.position, YS.position - YJJ * (hanglie.x - i));
            Gizmos.DrawLine((i - 1) * YJJ + bottomRedLine, upRedLine - (hanglie.x - i) * YJJ);
        }
        Gizmos.DrawLine((hanglie.x - 1) * YJJ + bottomRedLine, upRedLine);
    }
    public void saveZomLine()
    {
        for (int i = 0; i < dadie.transform.childCount; i++)
        {
            LvManager.Instance.wlist.Add(dadie.transform.GetChild(i).gameObject);
        }
        Debug.Log(LvManager.Instance.wlist.Count);
        foreach (GameObject gm in LvManager.Instance.wlist)
        {
            //Debug.Log(gm.transform.childCount);
            for (int i = 0; i < gm.transform.childCount; i++)
            {
                int djgg, djbb, djhh;
                ZomPos zm = gm.transform.GetChild(i).gameObject.GetComponent<ZomPos>();
                djgg = zm.djg;
                djbb = zm.djb;
                djhh = zm.djh;
                Debug.Log(djbb + "," + djhh);
                Debug.Log(LvManager.Instance.waves.Count);
                LvManager.Instance.waves[djbb].hang[djhh].ztp.Clear();
                //                Debug.Log(LvManager.Instance.gq[LvManager.Instance.gqs].waves[djbb].hang[djhh].ztp[0].name);
            }
        }
        foreach (GameObject gm in LvManager.Instance.wlist)
        {
            Debug.Log(gm.transform.childCount);
            for (int i = 0; i < gm.transform.childCount; i++)
            {
                int djbb, djhh;
                ZomPos zm = gm.transform.GetChild(i).gameObject.GetComponent<ZomPos>();
                //djgg = zm.djg;
                djbb = zm.djb;
                djhh = zm.djh;
                //if(zm.willDlt)
                //LvManager.Instance.gq[LvManager.Instance.gqs].waves[djbb].hang[djhh].ztp.Clear();
                //                Debug.Log(LvManager.Instance.gq[LvManager.Instance.gqs].waves[djbb].hang[djhh].ztp[0].name);
                if (!zm.willDlt)
                    LvManager.Instance.waves[djbb].hang[djhh].ztp.Add(new Ztype(zm.nameZ, 1, Mathf.Abs(zm.transform.position.x - transform.position.x) / 0.2760316f, 1, zm.ztpp, Mathf.Abs(zm.transform.position.x - transform.position.x)));
            }
        }
        Debug.Log(LvManager.Instance.waves);
        Debug.Log(LvManager.Instance.gqs.text.ToString());
        Saver.Instance.SaveByJSON(LvManager.Instance.waves, LvManager.Instance.gqs.text.ToString());
    }
}
