using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zxj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(transform.position);
        Debug.Log(GetComponent<RectTransform>().position);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
