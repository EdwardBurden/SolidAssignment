using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public abstract class GameItemEditor : Editor
{
    private static Texture2D Icon;

    public static void SetIcon(string Iconname)
    {
        if (!string.IsNullOrEmpty(Iconname))
        {
            string fullPath = Path.Combine(DataHandler.IconFilePath, Iconname);
            Icon = (Texture2D)AssetDatabase.LoadAssetAtPath(fullPath, typeof(Texture2D));
        }
        else
            Icon = null;
    }

    public static string GetImageName()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("GameItem Icon");
        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        Icon = (Texture2D)EditorGUILayout.ObjectField(Icon, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        if (Icon != null)
            return Path.GetFileName(AssetDatabase.GetAssetPath(Icon.GetInstanceID()));
        else
            return string.Empty;
    }
}
