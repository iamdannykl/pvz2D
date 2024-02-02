using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public static UImanager Instance;
    public Text SunNums;
    private UIPlantCards currentCard;
    public UIPlantCards CurrentCard
    {
        get => currentCard;
        set
        {
            if (currentCard != null)
            {
                if (Shovel.Instance.WantShovel)
                {
                    Shovel.Instance.WantShovel = false;
                    Shovel.Instance.desTiao();
                }
                currentCard.WantPlace = false;
                if (currentCard.HengTiao != null || currentCard.ShuTiao != null)
                {
                    Destroy(currentCard.HengTiao);
                    Destroy(currentCard.ShuTiao);
                }
            }
            currentCard = value;
        }
    }
    private void Awake() 
    {
        Instance=this;
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(BossManager.Instance.SunNum);
    }

    public void UpdateSunNum(int num)
    {
        SunNums.text=num.ToString();
    }

    //return the position of the Sum Number;
    public Vector3 GetSunNumPosition()
    {
        return SunNums.transform.position;
    }
}
