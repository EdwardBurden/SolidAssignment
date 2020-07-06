using System;
using System.Collections;
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
    }

    public static Ship CreateShip()
    {
        return new Ship() { Game_Icon = GetImageName(), Item_Type = ItemType.Spaceship, Stats = GetStats() };
    }

    private static Dictionary<StatType, int> GetStats()
    {
        if (Model.Stats != null && Model.Stats.Count > 0)
        {
            foreach (var item in Model.Stats)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(item.Key.ToString());
                Model.Stats[item.Key] = EditorGUILayout.IntField(item.Value);
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

        return Model.Stats;
    }
}
