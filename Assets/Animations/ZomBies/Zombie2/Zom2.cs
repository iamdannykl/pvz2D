using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zom2 : ZomPos
{
    // Start is called before the first frame update
    /*void Start()
    {
        Hp = 20;
    }*/

    // Update is called once per frame
    void Update()
    {

        if (downGrid != null)
            if (downGrid.NowCTM == null)
            {
                eat.Stop();
            }
        moveZom();
        jiaoxia();
        //Debug.Log(GridManager.Instance.returnGridByPoint(new Vector2(8,0)).Zombie);
        if (i >= 0 && i < 9) { Xb = i; hang = 0; }
        if (i >= 9 && i < 18) { Xb = i - 9; hang = 1; }
        if (i >= 18 && i < 27) { Xb = i - 18; hang = 2; }
        if (i >= 27 && i < 36) { Xb = i - 27; hang = 3; }
        if (i >= 36 && i < 45) { Xb = i - 36; hang = 4; }


    }
}
