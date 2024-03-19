using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kuangS : MonoBehaviour
{
    public int selfGqs;
    public GameObject pn;
    public GuanTP guanTP;
    public GameObject 关卡类型窗口;
    public void setGqs()
    {
        //PlayerPrefs.SetString("gameMode", "gamer");
        PlayerPrefs.SetInt("gq", selfGqs);
        Time.timeScale = 1;
        switch (guanTP)
        {
            case GuanTP.grass:
                SceneManager.LoadScene("SampleScene"); break;
            case GuanTP.pool:
                SceneManager.LoadScene("Pool"); break;
            default:
                break;
        }
    }
    public void 打开窗口()
    {
        if (PlayerPrefs.GetString("gameMode") == "editor")
        {
            关卡类型窗口.SetActive(true);
        }
        else
        {
            setGqs();
        }
    }
}