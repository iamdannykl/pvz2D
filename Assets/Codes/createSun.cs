using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class createSun : MonoBehaviour
{
    public static createSun Instance;
    public GameObject ze, one, two, three, four, five, six;
    float suijishu;
    public float jianGe;


    private void Awake()
    {
        Instance = this;
    }
    // Update is called once per frame



    public void dingShi()
    {
        InvokeRepeating("createIt", 5f, jianGe);
    }

    void createSunByPoolManager(GameObject pos)
    {
        sun sun = PoolManager.Instance.GetObject(BossManager.Instance.GameConf.Sun).GetComponent<sun>();
        sun.Find();
        sun.isSunFlowerCrt = false;
        sun.transform.position = pos.transform.position;
        sun.Yundong = true;
        sun.cl.color = new Color(1f, 1f, 1f, 1f);
    }
    //生成suns
    void createIt()
    {
        suijishu = Random.Range(0, 7);
        switch (suijishu)
        {
            case 0:
                createSunByPoolManager(ze);
                break;
            case 1:
                createSunByPoolManager(one);
                break;
            case 2:
                createSunByPoolManager(two);
                break;
            case 3:
                createSunByPoolManager(three);
                break;
            case 4:
                createSunByPoolManager(four);
                break;
            case 5:
                createSunByPoolManager(five);
                break;
            case 6:
                createSunByPoolManager(six);
                break;
            default:
                Debug.Log("ERROR");
                break;

        }
    }
}
