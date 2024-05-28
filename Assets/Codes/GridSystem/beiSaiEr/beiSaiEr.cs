using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class beiSaiEr : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Transform> pos = new List<Transform>();
    public GameObject ball;
    public GameObject dot;
    bool isUpdt;
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

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && pos.Count < 4)
        {
            //            Debug.Log("sss");
            // 获取鼠标在屏幕上的位置
            Vector3 mousePos = Input.mousePosition;
            // 将鼠标在屏幕上的位置转换为世界空间中的位置
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            GameObject posBall = Instantiate(ball, new Vector3(worldPos.x, worldPos.y, 0), Quaternion.identity);
            posBall.GetComponent<PosBall>().faza = gameObject;
            posBall.transform.SetParent(gameObject.transform);
            pos.Add(posBall.transform);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (pos.Count > 3)
        {
            for (int i = 0; i < 4; i++)
            {
                if (pos[i].GetComponent<PosBall>().isOver)
                {
                    isUpdt = true;
                    break;
                }
                if (i == 3 && !pos[3].GetComponent<PosBall>().isOver)
                {
                    isUpdt = false;
                }
            }
            if (isUpdt)
            {
                for (int i = 0; i < 101; i++)
                {
                    data[i] = Bezier(pos[0].position, pos[1].position, pos[2].position, pos[3].position, k[i] / 100);
                    dots[i].transform.position = data[i];
                }
            }
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
