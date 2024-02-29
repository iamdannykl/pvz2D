using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    private int sunNum;
    public GameConf GameConf { get; private set; }

    public GameObject yi, er, san, si, wu;

    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UImanager.Instance.UpdateSunNum(sunNum);
        }
    }
    private void Awake()
    {
        Instance = this;
        GameConf = Resources.Load<GameConf>("GameConf");
    }
    void Start()
    {
        SunNum = 50;
    }

    // Update is called once per frame




}
