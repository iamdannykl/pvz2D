using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSc : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject guanQia, MapType;
    bool isGq, isTp;
    public void startGame()
    {
        isGq = !isGq;
        guanQia.SetActive(isGq);
        //BossManager.Instance.GameModeCurrent = BossManager.GameMode.gamer;
        PlayerPrefs.SetString("gameMode", "gamer");
        Time.timeScale = 1;
        //SceneManager.LoadScene("SampleScene");
    }
    public void 关闭类型窗口()
    {
        MapType.SetActive(false);
        //SceneManager.LoadScene("SampleScene");
    }
    public void shutGq()
    {
        isGq = false;
        guanQia.SetActive(false);
    }
    public void editModeStart()
    {
        //BossManager.Instance.GameModeCurrent = BossManager.GameMode.editor;
        /*PlayerPrefs.SetString("gameMode", "editor");
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");*/
        isGq = !isGq;
        guanQia.SetActive(isGq);
        //BossManager.Instance.GameModeCurrent = BossManager.GameMode.gamer;
        PlayerPrefs.SetString("gameMode", "editor");
        Time.timeScale = 1;
        //SceneManager.LoadScene("SampleScene");
    }
    public void backStartGame()
    {
        PoolManager.Instance.CleanAllData();
        SceneManager.LoadScene("startSc");
    }
    public void quitGame()
    {
        Application.Quit();
    }
    //    void Start() => Debug.Log(BossManager.Instance.amns);
}
