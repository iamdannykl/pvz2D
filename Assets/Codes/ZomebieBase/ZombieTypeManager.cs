using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ZombieType
{
    normal,
    luZhang,
    tieTong,
    allStar,
    haiTunZom,
    iceCar
}

public class ZombieTypeManager : MonoBehaviour
{
    public static ZombieTypeManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject GetZombieFromType(ZombieType type)
    {
        switch (type)
        {
            case ZombieType.normal:
                return BossManager.Instance.GameConf.Zombie;
            case ZombieType.luZhang:
                return BossManager.Instance.GameConf.LzZombie;
            case ZombieType.tieTong:
                return BossManager.Instance.GameConf.tieTongZombie;
            case ZombieType.allStar:
                return BossManager.Instance.GameConf.allStar;
            case ZombieType.haiTunZom:
                return BossManager.Instance.GameConf.haiTunZom;
            case ZombieType.iceCar:
                return BossManager.Instance.GameConf.iceCar;
        }
        return null;

    }
}
