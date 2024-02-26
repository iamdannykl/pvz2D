using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeJS : MonoBehaviour
{
    public float timeNow;
    public float timeChiXu;
    public bool isBegin;
    public bool isFns;
    // Start is called before the first frame update
    public void InitTime()
    {
        timeNow = 0f;
        isBegin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBegin)
        {
            timeNow += Time.deltaTime;
        }
        if (timeNow >= timeChiXu)
        {
            isFns = true;
            timeNow = 0f;
            isBegin = false;
        }
    }
}
