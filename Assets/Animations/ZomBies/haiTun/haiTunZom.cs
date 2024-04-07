using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class haiTunZom : ZomPos
{
    protected override void Update()
    {
        base.Update();
        if (downGrid != null)
        {
            if (!downGrid.Plant)
            {
                eat.Stop();
            }
        }
        moveZom();
        jiaoxia();
    }
}
