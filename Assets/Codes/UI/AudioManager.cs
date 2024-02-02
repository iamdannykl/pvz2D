using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Start is called before the first frame update
    public AudioSource plant2;
    public AudioSource bg;
    public AudioSource plant;
    
    void Start()
    {
        BG();
    }

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlantIt()
    {
        int a=Random.Range(0,2);
        switch (a)
        {
            case 0:
                plant.Play();
                break;
            case 1:
                plant2.Play();
                break;
        }
    }

    public void BG()
    {
        bg.Play();
    }
    
}
