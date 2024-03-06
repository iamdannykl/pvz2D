using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    public static Saver Instance;
    int otpt;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public SaveContent CreateSave()
    { //创建一个Save对象存储当前游戏数据
        SaveContent save = new SaveContent();
        save.a = 8;
        return save;
    }
    //不需要加别的命名空间，简直爽死(bushi
    public void SaveByJSON()
    {
        SaveContent save = CreateSave();
        //创建一个Save实例存储游戏数据(CreateSave函数在上面)
        string JsonString = JsonUtility.ToJson(save);
        //将对象save转化为json字符串
        //上面说了Json是string类型，所以命名string
        StreamWriter sw = new StreamWriter(Application.dataPath + "/Data.yj");
        //persistentDataPath是隐藏文件的，所以你找不到Data.yj的所在地，
        //而dataPath就不会隐藏，同时文件后缀也可以乱取
        sw.Write(JsonString);
        //将json字符串写入流参数
        sw.Close();
        //把流关了
    }
    public void LoadByJSON()
    {
        if (File.Exists(Application.dataPath + "/Data.yj"))
        //判断文件是否创建
        {
            StreamReader sr = new StreamReader(Application.dataPath + "/Data.yj");
            //从流中读取字符串
            string JsonString = sr.ReadToEnd();
            //ReadToEnd()方法可以读取从流当前位置到结尾的所有字符
            //还有Read()方法，但是只读了一个字符，还有更多方法捏懒得打了
            sr.Close();
            //把流关了
            SaveContent save = JsonUtility.FromJson<SaveContent>(JsonString);
            //该方法属于泛型方法T，需要给出明确的类型定义，所以要写<Save>
            otpt = save.a;
            text.text = otpt.ToString() + Application.dataPath;
            /*GameManager.Instance.coins = save.coins;
            player.transform.position = new Vector2(save.playerPosition.x, save.playerPositionY);
            */
            //属于是常规方式了
        }
        else
        {
            Debug.LogError("File Not Found.");
        }
    }
}
