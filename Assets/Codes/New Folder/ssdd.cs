using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ssdd : MonoBehaviour
{
    private NewBehaviourScript newbhv;
    private void Start()
    {
        newbhv = new NewBehaviourScript
        {
            wsnd = "wwww"
        };
        Debug.Log(newbhv.wsnd);
    }
}
