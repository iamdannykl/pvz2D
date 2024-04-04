using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GqKuang : MonoBehaviour
{
    public int selfGqs;
    public GameObject pn;
    public void setGqs()
    {
        Debug.Log(LvManager.Instance.glx[PlayerPrefs.GetInt("Gtype", 0)].gq.Count);
        //LvManager.Instance.gqs = selfGqs;
        //LvManager.Instance.glx[PlayerPrefs.GetInt("Gtype", 0)].gq[LvManager.Instance.gqs].waves.Add(new Waves("第" + (LvManager.Instance.waveNowInEdit + 1) + "波", LvManager.Instance.EditHangShu));
        pn.SetActive(false);
        //Saver.LoadByJSON(selfGqs);
        LvManager.Instance.foreachZ();
        LvManager.Instance.WaveNowInEdit = 0;
    }
}
