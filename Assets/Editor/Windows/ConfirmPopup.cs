using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ConfirmPopup : EditorWindow
{
    public static Action ConfirmCallBack;

    public static void Init()
    {
        ConfirmPopup window = ScriptableObject.CreateInstance<ConfirmPopup>();
        window.position = new Rect(Screen.width / 2, Screen.height / 2,  200, 80);
        window.ShowPopup();
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Are you sure?", EditorStyles.wordWrappedLabel);
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Yes"))
        {
            ConfirmCallBack?.Invoke();
            this.Close();
        }
        if (GUILayout.Button("No"))
        {
            this.Close();
        }
        EditorGUILayout.EndHorizontal();
    }
}