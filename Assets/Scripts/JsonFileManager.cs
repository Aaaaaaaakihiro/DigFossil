using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonFileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreatJson();
        ReadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreatJson()
    {
        FileInfo fileInfo = new FileInfo(Application.dataPath + "/Data.json");
        if (fileInfo.Exists == false) fileInfo.Create();

        using (StreamWriter writer = new StreamWriter(Application.dataPath + "/Data.json"))
        {
            writer.Write("{id : 0}");
        }
    }

    public void ReadJson()
    {
        string str;
        using (StreamReader reader = new StreamReader(Application.dataPath + "/Data.json"))
        {
            str = reader.ReadToEnd();
            reader.Close();

            Debug.Log(str);
        }
    }
}
