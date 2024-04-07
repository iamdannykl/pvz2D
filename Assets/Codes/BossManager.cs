using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BossManager : MonoBehaviour
{
    public static BossManager Instance;
    public int amns = 3;
    private int sunNum;
    public GameConf GameConf { get; private set; }

    public GameObject yi, er, san, si, wu;
    public GameObject cameraM;
    public GameObject huiMu;
    public GameObject editorUI;
    public GameObject ZhongZhiUI;
    private ZomCardBase currentZCard;
    public ZomCardBase CurrentZCard
    {
        get => currentZCard;
        set
        {
            if (currentZCard != null)
            {
                currentZCard.WantPlace = false;
            }
            currentZCard = value;
        }
    }
    public enum GameMode
    {
        editor,
        gamer,
        beforeStart,
        nullMode
    }
    public GameMode gameModeCurrent;
    private GameMode gmc;
    public GameMode GameModeCurrent
    {
        get => gameModeCurrent;
        set
        {
            if (gmc == value) return;
            //            Debug.Log("dsadsa");
            gmc = value;
            StartCoroutine(switchGameMode(value));
        }
    }
    private void Update()
    {
        GameModeCurrent = gameModeCurrent;
        if (gameModeCurrent == GameMode.editor && Input.GetMouseButtonUp(1))
        {
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(myRay.origin.x, myRay.origin.y), Vector2.down);
            GameObject gameObj = hit.collider.gameObject;
            if (hit.collider.tag == "zom" && gameModeCurrent == GameMode.editor)
            {
                //PoolManager.Instance.SetInPool(ZombieTypeManager.Instance.GetZombieFromType(gameObj.GetComponent<ZomPos>().ztpp), gameObj);
                gameObj.GetComponent<ZomPos>().willDlt = true;
                gameObj.SetActive(false);
            }
        }
    }

    IEnumerator switchGameMode(GameMode gameModeCurrent)
    {
        yield return new WaitForSeconds(0.2f);
        switch (gameModeCurrent)
        {
            case GameMode.editor:
                LvManager.Instance.isBegin = false;
                LvManager.Instance.foreachZ();
                huiMu.SetActive(true);
                ZhongZhiUI.SetActive(false);
                editorUI.SetActive(true);
                cameraM.GetComponent<Animator>().Play("youyi");
                break;
            case GameMode.gamer:
                huiMu.SetActive(false);
                editorUI.SetActive(false);
                ZhongZhiUI.SetActive(true);
                cameraM.GetComponent<Animator>().Play("zuoyi");
                SunNum = 50;
                createSun.Instance.dingShi();
                LvManager.Instance.gameStart();
                break;
            case GameMode.beforeStart:
                LvManager.Instance.isBegin = false;
                editorUI.SetActive(false);
                huiMu.SetActive(false);
                ZhongZhiUI.SetActive(false);
                cameraM.GetComponent<Animator>().Play("youyi");
                break;
        }
        yield break;
    }
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UImanager.Instance.UpdateSunNum(sunNum);
        }
    }
    GameMode stringToGmd(string str)
    {
        if (str == "editor")
        {
            return GameMode.editor;
        }
        else if (str == "gamer")
        {
            return GameMode.gamer;
        }
        else if (str == "beforeStart")
        {
            return GameMode.beforeStart;
        }
        else
        {
            return GameMode.nullMode;
        }
    }
    private void Awake()
    {
        Instance = this;
        gmc = GameMode.nullMode;
        gameModeCurrent = stringToGmd(PlayerPrefs.GetString("gameMode", gameModeCurrent.ToString()));
        GameModeCurrent = gameModeCurrent;
        GameConf = Resources.Load<GameConf>("GameConf");
    }


    // Update is called once per frame




}
