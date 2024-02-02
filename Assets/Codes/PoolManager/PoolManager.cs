using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager//对象池
{
    private static PoolManager instance;
    private GameObject poolObj;

    public static PoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PoolManager();
            }
            return instance;
        }
    }

    private Dictionary<GameObject, List<GameObject>> poolDic = new Dictionary<GameObject, List<GameObject>>();

    public GameObject GetObject(GameObject prefab)
    {
        GameObject obj = null;
        if (poolDic.ContainsKey(prefab) && poolDic[prefab].Count > 0)
        {
            obj = poolDic[prefab][0];//返回List中的第一个
            poolDic[prefab].RemoveAt(0);//移出List中的第一个
        }
        else
        {
            obj = GameObject.Instantiate(prefab);
        }
        obj.SetActive(true);
        obj.transform.SetParent(null);
        return obj;
    }

    public void SetInPool(GameObject prefab,GameObject obj)
    {
        if (poolObj == null)//判断有没有根目录
        {
            poolObj = new GameObject("PoolObj");
        }

        if (poolDic.ContainsKey(prefab)) //判断字典有没有这个预制体的数据
        {
            poolDic[prefab].Add(obj);//把物体放进去
            //如果根目录下没有这个预制体命名的子物体
            if (poolObj.transform.Find(prefab.name) == false)
            {
                new GameObject(prefab.name).transform.SetParent(poolObj.transform);
            }
        }
        else //字典里面没有捏
        {//创建这个预制体的缓存池数据
            poolDic.Add(prefab,new List<GameObject>(){obj});
        }
        obj.SetActive(false);
        obj.transform.SetParent(poolObj.transform.Find(prefab.name));
    }
//清除所有的数据=====================Clear All Data    
    public void CleanAllData()
    {
        poolDic.Clear();
    }
}
