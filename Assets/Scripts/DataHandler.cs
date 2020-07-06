using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class DataHandler
{
    public const string RealPath = "Assets/Resources/Data/data.json";
    public const string FilePath = "Assets/data.json";
    public static List<Item> Import()
    {
        string jsonString = File.ReadAllText(RealPath);
        return JsonConvert.DeserializeObject<List<Item>>(jsonString, new ItemConverter());
    }

    public static void Export(List<Item> exportdata)
    {
        string jsondata = JsonConvert.SerializeObject(exportdata);
        File.WriteAllText(FilePath, jsondata);
    }


}
