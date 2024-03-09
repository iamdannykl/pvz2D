using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Waves
{
    public string name;
    public List<Hang> hang = new List<Hang>();
    public Waves(string str, int hangShu)
    {
        name = str;
        for (int i = 0; i < hangShu; i++)
        {
            hang.Add(new Hang());
            Debug.Log(hang);
        }
        //        hang.Add(new Hang());
    }
    //[SerializeField] string[] strings;
}
