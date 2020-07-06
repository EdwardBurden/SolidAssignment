using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GameItemEditor : Editor
{
    private static Texture2D Icon;
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
