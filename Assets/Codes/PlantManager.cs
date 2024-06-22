using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlantType
{
    SunFlower,
    PeaShooter,
    Cherry,
    Nut,
    Shovel,
    DaPenGu,
    swordWoman,
    spinePea,
    spineSunFler,
    DSpea,
    PotatoDL,
    snowPea,
    rePeater,
    SRH,
    heYe,
    cabage,
    XianRenZhang
}
public enum bulletType
{
    pea,
    snowPeaBullet,
    cabageBullet,
    Spike
}

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public static GameObject GetBulletFromType(bulletType type)
    {
        switch (type)
        {
            case bulletType.pea:
                return BossManager.Instance.GameConf.pea;
            case bulletType.snowPeaBullet:
                return BossManager.Instance.GameConf.snowPeaBlt;
            case bulletType.cabageBullet:
                return BossManager.Instance.GameConf.cabageBullet;
            case bulletType.Spike:
                return BossManager.Instance.GameConf.Spike;
        }
        return null;
    }
    public GameObject GetPlantFromType(PlantType type)
    {
        switch (type)
        {
            case PlantType.SunFlower:
                return BossManager.Instance.GameConf.SunFlower;
            case PlantType.PeaShooter:
                return BossManager.Instance.GameConf.PeaShooter;
            case PlantType.Cherry:
                return BossManager.Instance.GameConf.Cherry;
            case PlantType.Nut:
                return BossManager.Instance.GameConf.Nut;
            case PlantType.Shovel:
                return BossManager.Instance.GameConf.Shovel;
            case PlantType.DaPenGu:
                return BossManager.Instance.GameConf.DaPenGu;
            case PlantType.swordWoman:
                return BossManager.Instance.GameConf.swordWoman;
            case PlantType.spinePea:
                return BossManager.Instance.GameConf.spinePea;
            case PlantType.spineSunFler:
                return BossManager.Instance.GameConf.spineSunFler;
            case PlantType.DSpea:
                return BossManager.Instance.GameConf.DSpea;
            case PlantType.PotatoDL:
                return BossManager.Instance.GameConf.PotatoDL;
            case PlantType.snowPea:
                return BossManager.Instance.GameConf.snowPea;
            case PlantType.rePeater:
                return BossManager.Instance.GameConf.rePeater;
            case PlantType.SRH:
                return BossManager.Instance.GameConf.SRH;
            case PlantType.heYe:
                return BossManager.Instance.GameConf.heYe;
            case PlantType.cabage:
                return BossManager.Instance.GameConf.cabage;
            case PlantType.XianRenZhang:
                return BossManager.Instance.GameConf.XianRenZhang;
        }

        return null;
    }
}
