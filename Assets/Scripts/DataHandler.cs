using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class DataHandler
{
    public const string FilePath = "Assets/Resources/Data/data.json";
    public static List<Item> Import()
    {
        string jsonString = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<List<Item>>(jsonString);
    }

    public static void Export(Data exportdata)
    {
       // string jsondata = JsonConvert.SerializeObject(exportdata.Items);
        //File.WriteAllText(FilePath, jsondata);
    }
}
