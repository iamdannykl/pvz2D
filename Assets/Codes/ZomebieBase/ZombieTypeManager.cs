using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ZombieType
{
    normal,
    luZhang
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
        }
        return null;
        
    }
}
