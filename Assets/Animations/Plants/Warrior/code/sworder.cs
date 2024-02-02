using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sworder : CardTM
{
    public Vector2 size;
    private Ray2D ray;
    public float sheCheng;
    private GameObject target=null;
    public LayerMask enemy;
    [Header("数值")]
    public float hpSet;
    public int atkSet;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void XingWei()
    {
        
        hp = hpSet;
        //now=transform.position;
    }

    void checkIt()
    {
        ray = new Ray2D(grdPos, Vector2.right);
        RaycastHit2D info = Physics2D.Raycast(ray.origin, ray.direction,sheCheng);
        Debug.DrawLine(grdPos,grdPos+new Vector2(sheCheng,0),Color.yellow);
        //Debug.DrawRay(ray.origin,ray.direction,Color.blue);

        if (info.collider != null)
        {
            if (info.transform.gameObject.CompareTag("zom"))
            {
                target = info.transform.gameObject;
                anim.SetBool("isAtk",true);
            }
        }
        else
        {
            target = null;
            anim.SetBool("isAtk",false);
        }
    }

    public void slashIt()
    {
        attackEn.Play();
        if(target!=null)
        target.GetComponent<ZomPos>().ShanLiang();
        target.GetComponent<ZomPos>().Hp1 -= atkSet;
    }
    // Update is called once per frame
    void Update()
    {
        if (isUpdt)
        {
            checkIt();
        }
        //transform.position=now+new Vector3(pianYiX,pianYiY,0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(grdPos, size);
    }
}
