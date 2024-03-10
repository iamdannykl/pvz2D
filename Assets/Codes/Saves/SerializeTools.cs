using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class SerializeTools
{
    public static string ListToJson<T>(List<T> l)
    {
        return JsonUtility.ToJson(new Serialization<T>(l));
    }

    public static List<T> ListFromJson<T>(string str)
    {
        return JsonUtility.FromJson<Serialization<T>>(str).ToList();
    }

    public static string DicToJson<TKey, TValue>(Dictionary<TKey, TValue> dic)
    {
        return JsonUtility.ToJson(new Serialization<TKey, TValue>(dic));
    }

    public static Dictionary<TKey, TValue> DicFromJson<TKey, TValue>(string str)
    {
        return JsonUtility.FromJson<Serialization<TKey, TValue>>(str).ToDictionary();
    }

}
