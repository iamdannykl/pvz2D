using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mvtz : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform a, b;
    bool isa, isb;
    TimeJS tj;
    void Start()
    {
        tj = GetComponent<TimeJS>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-1.38f, 0) * Time.deltaTime / 5f);
        if (!isa && transform.position.x <= a.position.x)
        {
            isa = true;
            tj.InitTime();
        }
        if (!isb && tj.isFns)
        {
            isb = true;
            Debug.Log((a.position.x - transform.position.x) / tj.timeChiXu);
        }

    }
}

