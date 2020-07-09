using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SpaceShipEditor : GameItemEditor
{
    private static Ship Model = new Ship();

    public static void SetModel(Ship ship)
    {
        Model = ship;
        SetIcon(Model.Game_Icon);
    }

    public static Ship CreateShip(int item_Id, string item_Name)
    {
        string imageName = GetImageName();
        GetStats();
        return new Ship() { Item_Id = item_Id, Item_Name = item_Name, Game_Icon = imageName, Item_Type = ItemType.Spaceship, Stats = Model.Stats };
    }

    private static void GetStats()
    {
        if (Model.Stats != null && Model.Stats.Count > 0)
        {

            foreach (var key in Model.Stats.Keys.ToList())
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(key.ToString(), GUILayout.Width(200));
                Model.Stats[key] = EditorGUILayout.IntField(Model.Stats[key]);
                EditorGUILayout.EndHorizontal();
            }
        }
        else
        {
            if (Model.Stats == null)
                Model.Stats = new Dictionary<StatType, int>();
            for (int i = 0; i < Enum.GetNames(typeof(StatType)).Length; i++)
            {
                Model.Stats.Add((StatType)i, 10);
            }

        }
    }


}
