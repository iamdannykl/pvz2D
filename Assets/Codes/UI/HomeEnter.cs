using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeEnter : MonoBehaviour//其实这才是UI管理器
{
    public static HomeEnter Instance;
    public GameObject gameover;
    public GameObject Menu;
    public AudioSource overSd;
    bool isDbspd;
    private bool isMenuOn = false;
    private bool isOver = false;

    public bool isZanTing = false;

    public bool IsZanTing
    {
        get => isZanTing;
        set
        {
            isZanTing = value;
            if (isZanTing)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
    // Start is called before the first frame update
    public bool IsMenuOn
    {
        get => isMenuOn;
        set
        {
            isMenuOn = value;
            if (isMenuOn)
            {
                Menu.SetActive(true);
            }
            else
            {
                Menu.SetActive(false);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "zom")
        {
            if (!isOver)
            {
                Debug.Log("failed");
                AudioManager.Instance.bg.Stop();
                overSd.Play();
                gameover.SetActive(true);
                Invoke("setGameOverFalse", 5.8f);
                isOver = true;
            }
        }
    }

    public void setGameOverFalse()
    {
        gameover.SetActive(false);
        setMenuTrue();
        IsZanTing = true;
    }

    public void setMenuTrue()
    {
        IsMenuOn = true;
        IsZanTing = true;
    }
    public void setMenuFalse()
    {
        IsMenuOn = false;
        IsZanTing = false;
    }
    public void doubleSpd()
    {
        if (!isDbspd)
        {
            isDbspd = !isDbspd;
            Time.timeScale = 2f;
        }
    }
    void Awake()
    {
        Instance = this;
    }
}
