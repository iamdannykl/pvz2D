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

    [Header("UI")]
    public GameObject ShuTiao;
    public GameObject HengTiao;
    public GameObject Shovel;
    [Header("Zombies")]
    public GameObject Zombie;
    public GameObject LzZombie;
    public GameObject tieTongZombie;
}
