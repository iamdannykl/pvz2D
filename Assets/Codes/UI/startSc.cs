using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSc : MonoBehaviour
{
    // Start is called before the first frame update
    public void startGame()
    {
        //BossManager.Instance.GameModeCurrent = BossManager.GameMode.gamer;
        PlayerPrefs.SetString("gameMode", "gamer");
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void editModeStart()
    {
        //BossManager.Instance.GameModeCurrent = BossManager.GameMode.editor;
        PlayerPrefs.SetString("gameMode", "editor");
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
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
