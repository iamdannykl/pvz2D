using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class haiTunZom : ZomPos
{
    public Ray2D ray;
    public float sheCheng;
    public GameObject target = null;
    void checkIt()
    {
        Vector3 middlePoint = transform.position + new Vector3(0, 0.4f, 0);
        ray = new Ray2D(middlePoint, Vector2.left);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, sheCheng);
        Debug.DrawLine(middlePoint, new Vector2(middlePoint.x, middlePoint.y) - new Vector2(sheCheng, 0), Color.yellow);
        //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("plant"))
            {
                target = info.transform.gameObject;
                anim.SetBool("isJump", true);
            }
        }
        else
        {
            target = null;
            //anim.SetBool("isAtk", false);
        }
    }
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
        checkIt();
        moveZom();
        jiaoxia();
    }
}
