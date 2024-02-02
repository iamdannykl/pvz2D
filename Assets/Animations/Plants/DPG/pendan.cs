using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pendan : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start(){
        rb=GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(1f,0);//豌豆向右射去
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
    }
    void Update(){
        
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "zom")
        {
            ZomPos zomtgt = coll.gameObject.GetComponent<ZomPos>();
            zomtgt.Hp1 -= 1;
            zomtgt.ShanLiang();
        }
    }
    public void DesSelf(){
        Destroy(gameObject);
    }
}
