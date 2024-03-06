using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomGrid : MonoBehaviour
{
    public Transform YS;
    float YjianJu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        YjianJu = (YS.position.y - transform.position.y) / 5;
        Vector3 YJJ = new Vector3(0, YjianJu, 0);
        Gizmos.DrawWireCube((transform.position + YS.position) / 2, YS.position - transform.position);
        for (int i = 1; i <= 4; i++)
        {
            Gizmos.DrawLine(i * YJJ + transform.position, YS.position - YJJ * (5 - i));
        }
    }
}
