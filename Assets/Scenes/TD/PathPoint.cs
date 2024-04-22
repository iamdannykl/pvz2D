using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public float banJing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position,banJing);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
