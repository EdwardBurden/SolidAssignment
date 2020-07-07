using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class DataHandler
{
    public const string IconFilePath = "Assets/Resources/Textures";
    public const string OriginalDataPath = "Assets/Resources/Data/data.json";
    public const string DataPath = "Assets/data.json";

    public static List<Item> Import()
    {
        if (!File.Exists(OriginalDataPath))
        {
            FileStream stream = File.Create(OriginalDataPath);
            stream.Close();
        }

        string jsonString = File.ReadAllText(OriginalDataPath);
        return JsonConvert.DeserializeObject<List<Item>>(jsonString, new ItemConverter());

    }

    public static void Export(List<Item> exportdata)
    {
        string jsondata = JsonConvert.SerializeObject(exportdata);
        File.WriteAllText(DataPath, jsondata);
    }


}
