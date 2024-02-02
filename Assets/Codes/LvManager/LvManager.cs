using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LvManager : MonoBehaviour
{
    public static LvManager Instance;
    public Waves[] waves;
    //[SerializeField] string[] strings;
    public GameObject zero,one,two,three,four,five;

    public int waveNow;
    private int sameLineSortNum=0;
    public int allZomNum, allCrtZom;
    int hangShu, geShu;
    public float timePerWave;
    private float nowTime;
    public int plantSort;

    public int AllZomNum
    {
        get => allZomNum;
        set
        {
            allZomNum = value;
            if (allZomNum >= allCrtZom)
            {
                if (waveNow < waves.Length-1)
                {
                    waveNow++;
                    Invoke("BoShuCrt",3f);
                }
                else
                {
                    Debug.Log("Succeeded!");
                }
            }
        }
    }
    
    private void Awake()
    {
        Instance = this;
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

    private void Update()
    {
        if (Time.time - nowTime >= timePerWave)
        {
            if (waveNow < waves.Length-1)
            {
                ifChaoShi();
            }
        }
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
        allCrtZom = 0;
        allZomNum = 0;
        hangShu = waves[waveNow].hang.Length;
        for (int i = 0; i < hangShu; i++)//i为第几行zombie
        {
            geShu = waves[waveNow].hang[i].ztp.Length;
            for (int j = 0; j < geShu; j++)//j为第几种zombie
            {
                allCrtZom += waves[waveNow].hang[i].ztp[j].number;
                createZombie(waves[waveNow].hang[i].ztp[j].number,
                    trsf(i,j),
                    waves[waveNow].hang[i].ztp[j].delayTime,
                    waves[waveNow].hang[i].ztp[j].crtSpeed);
            }
        }
    }

    Vector2 trsf(int hang,int ge)
    {
        return new Vector2(hang, ge);
    }
    void createZombie(int num,Vector2 zom,float delay,float crtSpeed)
    {
        StartCoroutine(shengCheng(waveNow,num,zom,delay,crtSpeed));
    }

    private IEnumerator shengCheng(int wave,int num, Vector2 zom,float delay,float crtSpeed)
    {
        yield return new WaitForSeconds(delay);
        for (int i=num; i > 0; i--)
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
        switch(line)
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
            default:
                return new Vector3(0,0,0);  
        }
    }

    private void Start()
    {
        waveNow = 0;
        BoShuCrt();
    }
}
