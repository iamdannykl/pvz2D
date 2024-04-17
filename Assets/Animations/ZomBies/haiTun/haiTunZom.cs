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
        ray = new Ray2D(transform.position, Vector2.left);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, sheCheng);
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) - new Vector2(sheCheng, 0), Color.yellow);
        //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("plant"))
            {
                target = info.transform.gameObject;
                GetComponent<BoxCollider2D>().enabled = false;
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
