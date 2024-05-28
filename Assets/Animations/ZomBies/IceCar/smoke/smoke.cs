using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smoke : MonoBehaviour
{
    public void desIt()
    {
        Destroy(gameObject);
    }
    public void xianshi()
    {
        gameObject.SetActive(true);
    }
}
