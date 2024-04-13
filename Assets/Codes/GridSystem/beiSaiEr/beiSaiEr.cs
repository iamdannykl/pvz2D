using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beiSaiEr : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform a, b, c, d;
    public GameObject dot;
    List<GameObject> dots = new List<GameObject>();
    int N = 101;
    float[] k;
    List<Vector2> data = new List<Vector2>();
    void Awake()
    {
        k = chazhi(0, N, 1);
        for (int i = 0; i < 101; i++)
        {
            data.Add(new Vector2(0, 0));
            dots.Add(Instantiate(dot, data[i], Quaternion.identity));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < 101; i++)
        {
            data[i] = Bezier(a.position, b.position, c.position, d.position, k[i] / 100);
            dots[i].transform.position = data[i];
        }
    }
    float[] chazhi(int start, int end, float slice)
    {
        int xiangshu = (int)((end - start) * slice);
        float[] jieguo = new float[xiangshu];
        for (int i = 0; i < xiangshu; i++)
        {
            jieguo[i] = start + i * slice;
        }
        return jieguo;
    }
    public Vector2 Bezier(Vector2 A, Vector2 B, Vector2 C, Vector2 D, float k)
    {
        Vector2 t1 = A + (B - A) * k;
        Vector2 t2 = B + (C - B) * k;
        Vector2 t3 = C + (D - C) * k;
        Vector2 p1 = t1 + (t2 - t1) * k;
        Vector2 p2 = t2 + (t3 - t2) * k;
        Vector2 p3 = p1 + (p2 - p1) * k;
        return p3;
    }

}
