using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class kuangS : MonoBehaviour
{
    public int selfGqs;
    public GameObject pn;
    public void setGqs()
    {
        PlayerPrefs.SetString("gameMode", "gamer");
        PlayerPrefs.SetInt("gq", selfGqs);
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}