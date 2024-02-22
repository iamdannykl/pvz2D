using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snowPeaBullet : BulletBase
{
    // Start is called before the first frame update
    void unseen()
    {
        spr.material.color = new Color(0, 0, 0, 0);
    }
}
