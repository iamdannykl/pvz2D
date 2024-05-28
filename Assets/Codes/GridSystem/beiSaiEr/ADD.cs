using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADD : MonoBehaviour
{
    public GameObject xt;
    public void addXt()
    {
        if (danli.Instance.quXian.Count > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                danli.Instance.quXian[danli.Instance.quXian.Count - 1].transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        GameObject xtt = Instantiate(xt);
        //danli.Instance.quXian[danli.Instance.quXian.Count - 1].GetComponent<PosBall>().enabled = false;
        danli.Instance.quXian.Add(xtt);
        danli.Instance.nowWave += 1;
        Debug.Log(danli.Instance.nowWave);
    }
}
