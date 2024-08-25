using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class sun : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rb;
    int suiji;
    public Animator anim;
    public bool yundong = true;

    public AudioSource getsun;
    public SpriteRenderer cl;
    private bool isDianGuo;
    public bool isSunFlowerCrt;
    public float flyTime;




    // Start is called before the first frame update
    public void Find()
    {
        isDianGuo = false;
        rb = GetComponent<Rigidbody2D>();
        suiji = Random.Range(0, 5);
        cl = GetComponent<SpriteRenderer>();
    }

    public void jump()
    {
        int ran = Random.Range(0, 2);
        if (ran == 0) ran = -1;
        if (ran == 1) ran = 1;
        rb.velocity = new Vector2(2.5f * ran, 10);
        Invoke("stopYD", 1.4f);
    }
    // Update is called once per frame
    void Update()
    {
        downIt();
        if (yundong == true)
        {
            if (rb.velocity.y < -8f)
            {
                rb.velocity = new Vector2(rb.velocity.x, -8f);
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    void stopYD()
    {
        yundong = false;
    }

    void huimie()
    {
        CancelInvoke();
        StopAllCoroutines();//取消延迟调用和协程
        isSunFlowerCrt = false;
        //把自己放进缓存池
        PoolManager.Instance.SetInPool(BossManager.Instance.GameConf.Sun, gameObject);
    }

    public bool Yundong
    {
        get => yundong;
        set
        {
            yundong = value;
            if (yundong == true)
            {
                rb.velocity = new Vector2(rb.velocity.x, -5f);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
    }
    void downIt()
    {
        if (isSunFlowerCrt) return;
        switch (suiji)
        {
            case 0:
                if (transform.position.y < BossManager.Instance.er.transform.position.y)
                {
                    yundong = false;
                    Invoke("huimie", 8f);
                }
                break;
            case 1:
                if (transform.position.y < BossManager.Instance.er.transform.position.y)
                {
                    yundong = false;
                    Invoke("huimie", 8f);
                }
                break;
            case 2:
                if (transform.position.y < BossManager.Instance.san.transform.position.y)
                {
                    yundong = false;
                    Invoke("huimie", 8f);
                }
                break;
            case 3:
                if (transform.position.y < BossManager.Instance.si.transform.position.y)
                {
                    yundong = false;
                    Invoke("huimie", 8f);
                }
                break;
            case 4:
                if (transform.position.y < BossManager.Instance.wu.transform.position.y)
                {
                    yundong = false;
                    Invoke("huimie", 8f);
                }
                break;
            default:
                Debug.Log("ERROR");
                break;
        }
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (HomeEnter.Instance.isZanTing || (isDianGuo == true)) return;
            BossManager.Instance.SunNum += 50;
            Vector3 sunNumPos = UImanager.Instance.GetSunNumPosition();
            sunNumPos = new Vector3(sunNumPos.x, sunNumPos.y, 0);
            FlyBack(sunNumPos);
            isDianGuo = true;
        }
    }

    private void OnMouseDown()
    {
        if (HomeEnter.Instance.isZanTing || (isDianGuo == true)) return;
        BossManager.Instance.SunNum += 50;
        Vector3 sunNumPos = UImanager.Instance.GetSunNumPosition();
        sunNumPos = new Vector3(sunNumPos.x, sunNumPos.y, 0);
        FlyBack(sunNumPos);
        isDianGuo = true;
    }

    private void FlyBack(Vector3 pos)
    {
        getsun.Play();
        StartCoroutine(DoFly(pos));
    }

    private IEnumerator DoFly(Vector3 pos)
    {
        float dis = Vector3.Distance(pos, transform.position);
        float flySpd = dis / flyTime;
        while (Vector3.Distance(pos, transform.position) > 0.55f)
        {
            yield return new WaitForSeconds(0.016f);
            transform.Translate(flySpd * 0.016f * (pos - transform.position).normalized);
        }
        Yundong = false;
        cl.color = new Color(1f, 1f, 1f, 0f);
        Invoke("huimie", 3f);
    }


    public void toLeft()
    {
        anim.SetBool("isLeft", true);
    }
    public void toRight()
    {
        anim.SetBool("isRight", true);
    }
    public void toIdle()
    {
        anim.SetBool("isLeft", false);
        anim.SetBool("isRight", false);
    }

}
