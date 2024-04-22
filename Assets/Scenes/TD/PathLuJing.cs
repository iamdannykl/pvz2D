using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PathLuJing : MonoBehaviour
{
    public static PathLuJing Instance;
    public List<GameObject> PathPoints=new List<GameObject>();
    public List<float> quan = new List<float>();
    public GameObject enemy;
    public bool isLoop;
    public float EndSize;
    public GameObject enemyOne;

    private void Awake()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            PathPoints.Add(transform.GetChild(i).gameObject);
        }
        Instance = this;
    }

    private void OnDrawGizmos()
    {
        for(int i=1;i<transform.childCount;i++)
        {
            Gizmos.DrawLine(transform.GetChild(i-1).transform.position,transform.GetChild(i).transform.position);
        }
        Gizmos.color = new Color(1,0,0.2f);
        Gizmos.DrawWireSphere(transform.GetChild(transform.childCount - 1).transform.position,EndSize);
        Gizmos.color = new Color(0,0.8f,0.2f);
        Gizmos.DrawWireSphere(transform.GetChild(0).transform.position,EndSize);
    }

    void Start()
    {
        enemyOne=Instantiate(enemy, PathPoints[0].transform.position, quaternion.identity);
        //  Debug.Log(PathPoints[0].transform.position);
        enemyOne.GetComponent<Enemy>().placed();
        for (int i = 1; i < PathPoints.Count; i++)
        {
            quan.Add(Vector2.Distance(PathPoints[i].transform.position,PathPoints[i-1].transform.position));
        }
    }
}
