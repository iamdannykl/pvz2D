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
    DSpea
}

public class PlantManager : MonoBehaviour
{
    public static PlantManager Instance;
    private void Awake()
    {
        Instance = this;
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
        }
        
        return null;
    }
}
