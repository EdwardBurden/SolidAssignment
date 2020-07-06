using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameDataWindow : EditorWindow
{
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
        Data test = new Data();
        Ship p = new Ship();
        p.Stats = new Dictionary<StatType, int>();
        p.Stats.Add(StatType.power, 100);
        test.Items = new List<Item>();
        test.Items.Add(p);
        DataHandler.Export(test);
    }

    private void GetData()
    {
        var data = DataHandler.Import();
        Debug.Log(data);




    }
}

