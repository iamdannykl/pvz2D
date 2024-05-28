using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class front : MonoBehaviour
{
    public void frt()
    {
        if (danli.Instance.nowWave > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                danli.Instance.quXian[danli.Instance.nowWave].transform.GetChild(i).gameObject.SetActive(false);
                danli.Instance.quXian[danli.Instance.nowWave - 1].transform.GetChild(i).gameObject.SetActive(true);
            }
            danli.Instance.nowWave = danli.Instance.nowWave - 1;
            //Debug.Log(danli.Instance.nowWave);
        }
    }
    public void bhd()
    {
        if (danli.Instance.nowWave < danli.Instance.quXian.Count - 1)
        {
            for (int i = 0; i < 4; i++)
            {
                danli.Instance.quXian[danli.Instance.nowWave].transform.GetChild(i).gameObject.SetActive(false);
                danli.Instance.quXian[danli.Instance.nowWave + 1].transform.GetChild(i).gameObject.SetActive(true);
            }
            danli.Instance.nowWave = danli.Instance.nowWave + 1;
            //Debug.Log(danli.Instance.nowWave);
        }
    }
    public void printIt()
    {
        //savepos savepos = new savepos();

        int i = 0;
        foreach (var listT in danli.Instance.quXian)
        {
            Debug.Log("第" + (i + 1) + "个：");
            //savepos.posZu.Add(new zu());
            //zu zu = savepos.posZu[i];
            for (int j = 0; j < 4; j++)
            {/*
                if (j == 0)
                {
                    zu.x1 = listT.transform.GetChild(j).transform.position.x;
                    zu.y1 = listT.transform.GetChild(j).transform.position.y;
                }
                else if (j == 1)
                {
                    zu.x1 = listT.transform.GetChild(j).transform.position.x;
                    zu.y1 = listT.transform.GetChild(j).transform.position.y;
                }
                else if (j == 2)
                {
                    zu.x1 = listT.transform.GetChild(j).transform.position.x;
                    zu.y1 = listT.transform.GetChild(j).transform.position.y;
                }
                else if (j == 3)
                {
                    zu.x1 = listT.transform.GetChild(j).transform.position.x;
                    zu.y1 = listT.transform.GetChild(j).transform.position.y;
                }*/
                //zu.poszu.Add((Vector2)(listT.transform.GetChild(j).transform.position));
                //Debug.Log(listT.transform.GetChild(j).transform.position);
                Debug.Log((Vector2)(listT.transform.GetChild(j).transform.position));
            }
            i++;
        }
        //Debug.Log(savepos.posZu[0].poszu[0]);
        //Saver.SaveByJSON(savepos.posZu);
    }
}
