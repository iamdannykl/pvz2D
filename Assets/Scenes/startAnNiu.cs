using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class startAnNiu : MonoBehaviour
{
    public List<GameObject> gridList = new List<GameObject>();
    public Text tx;
    //public GameObject shadow;
    public Vector2 shuFirst, hengFirst;
    public GameObject anNiu;
    public GameObject die;

    private void Awake()
    {
        Saver.LoadByJSON();
        LvManager.Zgs = Saver.zgs;
        for (int i = 0; i < 20; i++)
        {
            GameObject gm = die.transform.GetChild(i).gameObject;
            gm.GetComponent<kuangS>().selfGqs = i;
            gridList.Add(gm);
            if (File.Exists(Application.dataPath + "/Data.json"))
            {
                if (i < Saver.zgs)
                {
                    /*if (i != 0)
                        LvManager.Instance.gq.Add(new GuanQia("第" + (i + 1) + "关"));*/
                    gm.SetActive(true);
                    gridList[i].name = "第" + (i + 1) + "关";
                    gridList[i].transform.GetChild(0).GetComponent<Text>().text = "第" + (i + 1) + "关";
                }
                else
                {
                    gm.SetActive(false);
                }
            }
        }
    }
    // Start is called before the first frame update


    public void AddEditGq()
    {
        if (LvManager.Zgs < gridList.Count)
        {
            gridList[LvManager.Zgs].SetActive(true);
            gridList[LvManager.Zgs].name = "第" + (LvManager.Zgs + 1) + "关";
            gridList[LvManager.Zgs].transform.GetChild(0).GetComponent<Text>().text = "第" + (LvManager.Zgs + 1) + "关";
            LvManager.Zgs++;
            LvManager.Instance.gq.Add(new GuanQia("第" + LvManager.Zgs + "关"));
        }
        else
        {
            Debug.LogError("超过列表最大容量");
        }
    }

    public void setGqs()
    {
        LvManager.Instance.gqs = LvManager.Zgs - 1;
    }
}