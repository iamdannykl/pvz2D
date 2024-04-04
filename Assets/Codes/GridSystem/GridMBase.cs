using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class GridMBase : MonoBehaviour
{
    public static GridMBase Instance;
    public List<GameObject> gridList = new List<GameObject>();
    public Text tx;
    //public GameObject shadow;
    public Vector2 shuFirst, hengFirst;
    public GameObject anNiu;
    public GameObject die;

    private void Awake()
    {
        Instance = this;
        Saver.LoadByJSON();
        LvManager.Zgs = Saver.zgs;
        for (int i = 0; i < 20; i++)
        {
            GameObject gm = die.transform.GetChild(i).gameObject;
            gm.GetComponent<GqKuang>().selfGqs = i;
            gridList.Add(gm);
            if (File.Exists(Application.dataPath + "/Data.json"))
            {
                if (i < Saver.zgs)
                {
                    if (i != 0)
                        LvManager.Instance.glx[PlayerPrefs.GetInt("Gtype", 0)].gq.Add(new GuanQia("第" + (i + 1) + "关"));
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
            LvManager.Instance.glx[PlayerPrefs.GetInt("Gtype", 0)].gq.Add(new GuanQia("第" + LvManager.Zgs + "关"));
        }
        else
        {
            Debug.LogError("超过列表最大容量");
        }
    }

    public void setGqs()
    {
        //LvManager.Instance.gqs = LvManager.Zgs - 1;
    }
}