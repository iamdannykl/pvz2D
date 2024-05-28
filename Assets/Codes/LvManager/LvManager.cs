using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class LvManager : MonoBehaviour
{
    public static LvManager Instance;
    public List<GuanLeiXing> glx = new List<GuanLeiXing>();
    public List<Waves> waves = new List<Waves>();
    [Header("sss")]
    public Text gqs;//第几关
    public string ggqs;
    public bool xuanKaWanBi;
    //[SerializeField] string[] strings;
    public GameObject zero, one, two, three, four, five;
    public GameObject xuanKaKuang;

    public bool isBegin;
    public static int Zgs;//一共多少关
    public int EditHangShu;

    public int waveNow;
    public int waveNowInEdit;
    public Text text;
    private int sameLineSortNum = 1;
    public int allZomNum, allCrtZom;
    int hangShu, geShu;
    public float timePerWave;
    private float nowTime;
    public int plantSort;
    public Transform fatherOb;
    public Transform zx;
    public List<GameObject> wlist = new List<GameObject>();
    public GameObject tou;
    public bool XuanKaWanBi
    {
        get => xuanKaWanBi;
        set
        {
            if (xuanKaWanBi == value) return;
            xuanKaWanBi = value;
            if (value)
            {
                createSun.Instance.dingShi();
                xuanKaKuang.GetComponent<Animator>().Play("kaishi");
                gameStart();
            }
        }
    }
    public int WaveNowInEdit
    {
        get => waveNowInEdit;
        set
        {
            text.text = "第" + (value + 1) + "波";
            waveNowInEdit = value;
        }
    }

    public int AllZomNum
    {
        get => allZomNum;
        set
        {
            allZomNum = value;
            if (allZomNum >= allCrtZom)
            {
                if (waveNow < waves.Count - 1)
                {
                    waveNow++;
                    Invoke("BoShuCrt", 3f);
                }
                else
                {
                    Debug.Log("Succeeded!");
                }
            }
        }
    }
    public void nextBo()//波数显示在窗口
    {
        fatherOb.GetChild(waveNowInEdit).gameObject.SetActive(false);
        WaveNowInEdit++;
        if (waveNowInEdit < waves.Count)
        {
            fatherOb.GetChild(waveNowInEdit).gameObject.SetActive(true);
            return;
        }
        waves.Add(new Waves("第" + (waveNowInEdit + 1) + "波", EditHangShu));
        GameObject waveEdit = new GameObject();
        waveEdit.name = "wave" + (waveNowInEdit + 1);
        waveEdit.transform.SetParent(fatherOb);
        //wlist.Add(waveEdit);
    }
    public void saveGqs()
    {
        //Saver.Instance.SaveByJSON();
    }

    private void Awake()
    {
        Instance = this;
        waves.Add(new Waves("第" + (waveNowInEdit + 1) + "波", EditHangShu));
        //Zgs = PlayerPrefs.GetInt("zongGQ", 1);
        /*for (int i = 0; i < 3; i++)
        {
            glx.Add(new GuanLeiXing());
        }
        for (int i = 0; i < Zgs; i++)
        {
            glx[PlayerPrefs.GetInt("Gtype", 0)].gq.Add(new GuanQia("第" + (i + 1) + "关"));
        }

        //wlist.Add(tou);

        
        WaveNowInEdit = waveNowInEdit;*/
    }
    public void foreachZ()
    {
        int bos = waves.Count;//波数
        for (int i = 1; i < bos; i++)
        {
            GameObject waveEdit = new GameObject();
            waveEdit.name = "wave" + (i + 1);
            waveEdit.transform.SetParent(fatherOb);
            waveEdit.SetActive(false);
        }
        for (int i = 0; i < bos; i++)
        {
            for (int j = 0; j < EditHangShu; j++)
            {
                foreach (Ztype zt in waves[i].hang[j].ztp)
                {
                    ZomPos zb = PoolManager.Instance.GetObject(ZombieTypeManager.Instance.GetZombieFromType(zt.zType)).GetComponent<ZomPos>();
                    zb.canMv = false;
                    zb.transform.position = new Vector2(zx.position.x + zt.distanceZ, ZomGrid.Instanse.lineList[j].ZomLineLeftPoint.y);
                    zb.transform.SetParent(fatherOb.transform.GetChild(i));
                    //zb.djg = gqs;
                    zb.djb = i;
                    zb.djh = j;
                }
            }

        }
    }
    public int SameLineSortNum
    {
        get => sameLineSortNum;
        set
        {
            sameLineSortNum = value;
            if (sameLineSortNum > 49)
            {
                sameLineSortNum = 1;
            }
        }
    }

    public int PlantSort
    {
        get => plantSort;
        set
        {
            plantSort = value;
            if (plantSort > 0)
            {
                plantSort = -49;
            }
        }
    }
    public void letsRock()
    {
        XuanKaWanBi = true;
    }
    public void xiajiang()
    {
        xuanKaKuang.SetActive(false);
    }
    private void Update()
    {
        if (isBegin && Time.time - nowTime >= timePerWave)
        {
            if (waveNow < waves.Count - 1)
            {
                ifChaoShi();
            }
        }
        //        Debug.Log(gqs);
        //wavess = glx[leiXingShu].gq[gqs].waves;
    }

    void ifChaoShi()
    {
        int shangBoCrtLeft = allCrtZom - allZomNum;
        allZomNum = 0;
        waveNow++;
        nowTime = Time.time;
        BoShuCrt();
        allCrtZom += shangBoCrtLeft;
    }

    // Start is called before the first frame update
    void BoShuCrt()//开始新的一波刷新
    {
        nowTime = Time.time;
        allCrtZom = 0;
        allZomNum = 0;
        hangShu = waves[waveNow].hang.Count;
        for (int i = 0; i < hangShu; i++)//i为第几行zombie
        {
            geShu = waves[waveNow].hang[i].ztp.Count;
            for (int j = 0; j < geShu; j++)//j为第几种zombie
            {
                allCrtZom += waves[waveNow].hang[i].ztp[j].number;
                createZombie(waves[waveNow].hang[i].ztp[j].number,
                    trsf(i, j),
                    waves[waveNow].hang[i].ztp[j].delayTime,
                    waves[waveNow].hang[i].ztp[j].crtSpeed);
            }
        }
    }

    Vector2 trsf(int hang, int ge)
    {
        return new Vector2(hang, ge);
    }
    void createZombie(int num, Vector2 zom, float delay, float crtSpeed)
    {
        StartCoroutine(shengCheng(waveNow, num, zom, delay, crtSpeed));
    }

    private IEnumerator shengCheng(int wave, int num, Vector2 zom, float delay, float crtSpeed)
    {
        yield return new WaitForSeconds(delay);
        for (int i = num; i > 0; i--)
        {
            ZomPos zombieType = /*Instantiate(ZombieTypeManager.Instance.GetZombieFromType(waves[wave].hang[(int)zom.x].ztp[(int)zom.y].zType),
                getV3ByLine((int)zom.x),
                quaternion.identity).GetComponent<ZomPos>();*/
            PoolManager.Instance.GetObject(ZombieTypeManager.Instance.GetZombieFromType(waves[wave].hang[(int)zom.x].ztp[(int)zom.y].zType)).GetComponent<ZomPos>();
            zombieType.gameObject.transform.position = getV3ByLine((int)zom.x);
            zombieType.Find((int)zom.x);
            zombieType.sortZom(sameLineSortNum);
            SameLineSortNum++;
            yield return new WaitForSeconds(crtSpeed);
        }
    }
    private Vector3 getV3ByLine(int line)
    {
        switch (line)
        {
            case 0:
                return zero.transform.position;
            case 1:
                return one.transform.position;
            case 2:
                return two.transform.position;
            case 3:
                return three.transform.position;
            case 4:
                return four.transform.position;
            case 5:
                return five.transform.position;
            default:
                return new Vector3(0, 0, 0);
        }
    }


    public void gameStart()
    {
        nowTime = Time.time;
        waveNow = 0;
        ggqs = PlayerPrefs.GetInt("gq", 0).ToString();
        //Debug.Log(gqs);
        Saver.LoadByJSON(ggqs);

        BoShuCrt();
        isBegin = true;
    }
}
