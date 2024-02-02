using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cherry : CardTM
{
    // Start is called before the first frame update
    private GameObject explode;
    private Renderer _color;
    public AudioSource ads;
    void Start()
    {
        _color = GetComponent<Renderer>();
        sunCost=150;
    }
    
    protected override void XingWei()
    {
        hp = 30000f;
        Invoke("explodeZom",0.5f);
    }

    void explodeZom()
    {
        _color.material.color = new Color(1,1,1,0);
        ads.Play();
        explode = transform.GetChild(0).gameObject;
        explode.SetActive(true);
        //Invoke("xiaoshi",1.1f);
    }

    public void xiaoshi()
    {
        explode.SetActive(false);
        GridManager.Instance.jiaoxiaGrid(transform.position).setPlant(false);
        Destroy(gameObject);
    }
}
