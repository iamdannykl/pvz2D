using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class HPmanager : MonoBehaviour
{
    public List<int> hpJieDuan;
    public int jieDuanShu;
    public int NowJieDuan;//为0时是满状态，为总阶段数-1时为die
    [HideInInspector] public Animator anim;
    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        jieDuanShu = hpJieDuan.Count;//获取hp状态个数。
    }
    public int JieDuanJianCe(int hp)
    {
        for (int i = 1; i <= jieDuanShu; i++)
        {
            if (hp > hpJieDuan[i])
            {
                return i - 1;
            }
            else if (hp > 0 && hp <= hpJieDuan[i])
            {
                continue;
            }
            else
            {
                return jieDuanShu - 1;
            }
        }
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetInteger("hurtLevel", NowJieDuan);
    }
}
