using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum bltType
{
    shoot, thrower
}
public class BulletBase : MonoBehaviour
{
    protected Rigidbody2D rb;
    public float hight;
    public float hangGao;
    public float xJuLi;
    public float graveIndex = 9.8f;
    protected Animator anim;
    public bulletType bulletType;
    public bltType bltType;
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
        if (bltType == bltType.shoot)
        {
            rb.velocity = new Vector3(10f, 0);//豌豆向右射去
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
        }
        if (bltType == bltType.thrower)
        {
            float vx, vy, timey;
            vy = (float)System.Math.Sqrt(2 * graveIndex * hight);
            timey = (float)System.Math.Sqrt(2 * hight / graveIndex);
            vx = xJuLi / (2 * timey);
            rb.velocity = new Vector3(vx, vy);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (gameObject.transform.position.x >= 7.2f)
        {
            Destroy(gameObject);
        }
        if (bltType == bltType.thrower && transform.position.y <= hangGao)
        {
            huimie();
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "zom" && canDamage)
        {
            if (canDamage)
            {
                if (bulletType != bulletType.Spike)
                    canDamage = false;
                ZomPos zomtgt = coll.gameObject.GetComponent<ZomPos>();
                switch (bulletType)
                {
                    case bulletType.pea:
                        zomtgt.Hp1 -= 1; break;
                    case bulletType.cabageBullet:
                        zomtgt.Hp1 -= 2; break;
                    case bulletType.snowPeaBullet:
                        zomtgt.Hp1 -= 1;
                        zomtgt.IsFrozen = true;
                        if (!zomtgt.isDong)
                        {
                            additionalSd.Play();
                            zomtgt.isDong = true;
                            Debug.Log("sddsdsdsdsdsdsd");
                        }
                        break;
                    case bulletType.Spike:
                        zomtgt.Hp1 -= 1; break;
                }
                zomtgt.ShanLiang();
                bulletSd.Play();
                if (bulletType != bulletType.cabageBullet && bulletType != bulletType.Spike)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    rb.velocity = new Vector2(coll.gameObject.GetComponent<Rigidbody2D>().velocity.x, -1f);
                    anim.SetBool("isbao", true);
                }
                if (bulletType == bulletType.cabageBullet)
                {
                    huimie();
                }

                /*if (additionalSd != null)
                {
                    additionalSd.Play();
                }*/

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
