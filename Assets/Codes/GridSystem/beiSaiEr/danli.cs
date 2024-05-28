using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class danli : MonoBehaviour
{
    public static danli Instance;
    public List<GameObject> quXian = new List<GameObject>();
    public int nowWave;
    private void Awake()
    {
        Instance = this;
    }

}
