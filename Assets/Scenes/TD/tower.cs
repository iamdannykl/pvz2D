
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tower : MonoBehaviour
{
    public float AttackSize;
    public List<GameObject> enemyList = new List<GameObject>();

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0);
        Gizmos.DrawWireSphere(transform.position,AttackSize);
    }*/

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count > 0)
        {
            target = enemyList[0].transform;
            transform.up = target.position - transform.position;
        }
        //Debug.Log( PathLuJing.Instance.enemyOne.transform.position - transform.position);
    }
}
