using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self : MonoBehaviour
{
    private Animator anim;
    public ZombieType zomTyp;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void padi()
    {
        anim.Play("padi");
    }
    // Start is called before the first frame update
    public void desz()
    {
        //Destroy(transform.parent);
        PoolManager.Instance.SetInPool(ZombieTypeManager.Instance.GetZombieFromType(zomTyp),gameObject.transform.parent.gameObject);
    }
}
