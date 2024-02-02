using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nut : CardTM
{
    void Start()
    {
        hp = 3000f;
        sunCost=50;
    }
    // Update is called once per frame
    void Update()
    {
        if (hp <= 1000)
        {
            anim.Play("btm");
        }
        else if (hp <= 2000)
        {
            anim.Play("mid");
        }
    }
}
