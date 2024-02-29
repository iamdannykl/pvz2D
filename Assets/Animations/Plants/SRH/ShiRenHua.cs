using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShiRenHua : CardTM
{
    TimeJS jsq;
    public Vector2 size;
    private Ray2D ray;
    public float sheCheng;
    private GameObject target = null;
    public LayerMask enemy;
    [Header("数值")]
    public float hpSet;
    public int atkSet;
    bool isJiaoZom;
    // Start is called before the first frame update
    void Start()
    {
        jsq = GetComponent<TimeJS>();
    }
    protected override void XingWei()
    {

        hp = hpSet;
        //now=transform.position;
    }

    void checkIt()
    {
        ray = new Ray2D(grdPos, Vector2.right);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction, sheCheng);
        Debug.DrawLine(grdPos, grdPos + new Vector2(sheCheng, 0), Color.yellow);
        //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("zom") && !isJiaoZom)
            {
                isJiaoZom = true;
                anim.SetBool("isZomQian", true);
                target = info.transform.gameObject;
            }
        }
        else
        {
            target = null;
            anim.SetBool("isZomQian", false);
        }
    }
    void yaoSui()
    {
        target.GetComponent<ZomPos>().CurrentState = ZomPos.State.disAppear;
    }
    public void enterJiao()
    {
        jsq.InitTime();
        anim.SetBool("isJiao", true);
    }
    public void backIdle()
    {
        jsq.isFns = false;
        isJiaoZom = false;
        anim.SetBool("isJiao", false);
    }
    public void EatIt()
    {
        attackEn.Play();
        if (target != null)
            target.GetComponent<ZomPos>().Hp1 = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (isUpdt)
        {
            checkIt();
            anim.SetBool("isFnsJiao", jsq.isFns);
        }
        //transform.position=now+new Vector3(pianYiX,pianYiY,0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(grdPos, size);
    }
}
