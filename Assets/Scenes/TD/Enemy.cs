using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float distanceToEnd;
    public GameObject nextNode;
    public GameObject lastNode;
    public float speedEnemy;
    public int nextNodeNum;
    public int lastNodeNum;
    private Vector2 fangXiang;
    public void placed()
    {
        nextNodeNum = 1;
        lastNodeNum = 0;
        nextNode = PathLuJing.Instance.PathPoints[nextNodeNum];
        lastNode=PathLuJing.Instance.PathPoints[lastNodeNum];
        fangXiang = (nextNode.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, nextNode.transform.position) >= PathLuJing.Instance.PathPoints[nextNodeNum].GetComponent<PathPoint>().banJing)
        {
            transform.Translate(fangXiang*speedEnemy);//Move
        }
        else if(nextNodeNum!=PathLuJing.Instance.PathPoints.Count-1)
        {
            nextNode = PathLuJing.Instance.PathPoints[++nextNodeNum];
            /*if (lastNodeNum == PathLuJing.Instance.PathPoints.Count - 1)
            {
                lastNodeNum = 0;
                lastNode = PathLuJing.Instance.PathPoints[lastNodeNum];
            }
            else
            {
                lastNode = PathLuJing.Instance.PathPoints[++lastNodeNum];
            }*/
            //fangXiang = (nextNode.transform.position - transform.position).normalized;
        }
        else
        {
            if (PathLuJing.Instance.isLoop)
            {
                nextNodeNum = 0;
                //lastNodeNum = PathLuJing.Instance.PathPoints.Count - 1;
                nextNode = PathLuJing.Instance.PathPoints[nextNodeNum];
                //lastNode = PathLuJing.Instance.PathPoints[lastNodeNum];
                //fangXiang = (nextNode.transform.position - transform.position).normalized;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        fangXiang = (nextNode.transform.position - transform.position).normalized;
        //distanceToEnd=
    }
}
