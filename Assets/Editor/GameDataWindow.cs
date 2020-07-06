using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataWindow : EditorWindow
{
    private Data MyData = new Data();

    [MenuItem("Window/GameDataWindow")]
    static void OpenWindow()
    {
        GameDataWindow window = (GameDataWindow)GetWindow(typeof(GameDataWindow));
        window.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Add Item"))
        {
            AddNew();
        }
        if (GUILayout.Button("Get Data"))
        {
            GetData();
        }
    }

    private void AddNew()
    {
        DataHandler.Export(MyData);
    }

    private void GetData()
    {
        MyData.Items = DataHandler.Import();





    }
}

