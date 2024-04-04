using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GQtype : MonoBehaviour
{
    public GuanTP gtp;
    // Start is called before the first frame update
    public void enterGrass()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Gtype", 0);
        SceneManager.LoadScene("SampleScene");
    }
    public void enterPool()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("Gtype", 2);
        SceneManager.LoadScene("Pool");
    }
}
