using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuoKaCao : MonoBehaviour
{
    public static ZuoKaCao Instance;
    // Start is called before the first frame update
    public int cardsNum;
    public int currentCardNum;
    //public List<GameObject> cards = new List<GameObject>();

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
