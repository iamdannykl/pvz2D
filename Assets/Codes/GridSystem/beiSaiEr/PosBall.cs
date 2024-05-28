using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

public class PosBall : MonoBehaviour
{
    public GameObject faza;
    public bool isOver;
    Vector3 realPos;
    private void Update()
    {
        if (isOver)
            transform.position = realPos;
    }
    private void OnMouseOver()
    {
        Vector3 mousePos = Input.mousePosition;
        // 将鼠标在屏幕上的位置转换为世界空间中的位置
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        realPos = new Vector3(worldPos.x, worldPos.y, 0);

        if (Input.GetMouseButtonUp(0))
        {
            isOver = !isOver;
        }
    }
}
