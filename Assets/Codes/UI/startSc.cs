using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startSc : MonoBehaviour
{
    // Start is called before the first frame update
    public void startGame()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale=1;
    }
    public void backStartGame()
    {
        SceneManager.LoadScene("startSc");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
