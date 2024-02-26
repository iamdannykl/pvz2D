using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    public bulletType bulletType;
    bool canDamage;
    public SpriteRenderer spr;
    private List<ZomPos> zomList;

    public AudioSource bulletSd;
    public AudioSource additionalSd;

    // Start is called before the first frame update
    void Start()
    {
        Find();

    }

    public void Find()
    {
        spr = GetComponent<SpriteRenderer>();
        //bulletSd = GetComponent<AudioSource>();
        spr.material.color = new Color(1, 1, 1, 1);
        canDamage = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        rb.velocity = new Vector3(10f, 0);//豌豆向右射去
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
    }
    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.x >= 7.2f)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "zom" && canDamage)
        {
            if (canDamage)
            {
                canDamage = false;
                ZomPos zomtgt = coll.gameObject.GetComponent<ZomPos>();
                switch (bulletType)
                {
                    case bulletType.pea:
                        zomtgt.Hp1 -= 1; break;
                    case bulletType.snowPeaBullet:
                        zomtgt.Hp1 -= 1;
                        zomtgt.IsFrozen = true;
                        break;
                }
                zomtgt.ShanLiang();
                bulletSd.Play();
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(coll.gameObject.GetComponent<Rigidbody2D>().velocity.x, -1f);
                anim.SetBool("isbao", true);
                if (additionalSd != null)
                {
                    additionalSd.Play();
                }

                //Invoke("huimie",0.35f);
            }
        }
    }

    /*
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "zom")
        {
            if (canDamage)
            {
                canDamage = false;
                ZomPos zomtgt = coll.gameObject.GetComponent<ZomPos>();
                zomtgt.Hp1 -= 1;
                zomtgt.ShanLiang();
                bulletSd.Play();
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                rb.velocity = new Vector2(coll.gameObject.GetComponent<Rigidbody2D>().velocity.x,-1f);
                anim.SetBool("isbao",true);
                Invoke("huimie",0.35f);
            }
        }
    }*/
    public void huimie()
    {
        PoolManager.Instance.SetInPool(PlantManager.GetBulletFromType(bulletType), gameObject);
    }
}
