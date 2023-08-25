using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [HideInInspector] public SaveData data;

    string filepath;
    string fileName = "Data.json";

    private void Awake()
    {
        if (Application.isEditor)
        {
            filepath = Application.dataPath + "/" + fileName;
        }
        else
        {
            filepath = Application.persistentDataPath + "/" + fileName;
        }

        if (!File.Exists(filepath))
        {
            Save();
        }

        data = Load();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(data);
        StreamWriter wr = new StreamWriter(filepath, false);
        wr.WriteLine(json);
        wr.Close();
    }

    public SaveData Load()
    {
        StreamReader rd = new StreamReader(filepath);
        string json = rd.ReadToEnd();
        rd.Close();

        return JsonUtility.FromJson<SaveData>(json);
    }
}
