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

    public static bool CanImport(string targetPath)
    {
        if (!string.IsNullOrWhiteSpace(targetPath) && File.Exists(targetPath))
        {
            try
            {
                string jsonString = File.ReadAllText(targetPath);
                if (!string.IsNullOrWhiteSpace(jsonString))
                {
                    var file = JsonConvert.DeserializeObject<List<Item>>(jsonString, new ItemConverter());
                    if (file != null)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        return false;
    }

    public static List<Item> Import(string targetPath)
    {
        string jsonString = File.ReadAllText(targetPath);
        return JsonConvert.DeserializeObject<List<Item>>(jsonString, new ItemConverter());
    }

    public static bool CanExport(string targetPath)
    {
        return true;
    }

    public static void Export(List<Item> exportdata , string path)
    {
        string jsondata = JsonConvert.SerializeObject(exportdata);
        File.WriteAllText(path, jsondata);
    }


}
