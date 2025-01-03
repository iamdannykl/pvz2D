using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConf", menuName = "GameConf")]
public class GameConf : ScriptableObject//��Ϸ����
{
    [Header("Objects")]
    public GameObject pea;
    public GameObject snowPeaBlt;
    public GameObject Sun;
    public GameObject penDan;
    public GameObject SRH;
    public GameObject cabageBullet;
    public GameObject Spike;

    [Header("Plants")]
    public GameObject SunFlower;
    public GameObject PeaShooter;
    public GameObject Cherry;
    public GameObject Nut;
    public GameObject DaPenGu;
    public GameObject swordWoman;
    public GameObject spinePea;
    public GameObject spineSunFler;
    public GameObject DSpea;
    public GameObject PotatoDL;
    public GameObject snowPea;
    public GameObject rePeater;
    public GameObject heYe;
    public GameObject cabage;
    public GameObject XianRenZhang;

    [Header("UI")]
    public GameObject ShuTiao;
    public GameObject HengTiao;
    public GameObject Shovel;
    [Header("Zombies")]
    public GameObject Zombie;
    public GameObject LzZombie;
    public GameObject tieTongZombie;
    public GameObject allStar;
    public GameObject haiTunZom;
    public GameObject iceCar;
}
