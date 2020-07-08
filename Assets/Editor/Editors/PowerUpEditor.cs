using System;
using UnityEditor;

public class PowerUpEditor : GameItemEditor
{
    private static PowerUp Model = new PowerUp();

    public static void SetModel(PowerUp model)
    {
        Model = model;
        SetIcon(Model.Game_Icon);
    }

    public static PowerUp CreatePowerUp(int item_Id, string item_Name)
    {
        string icon = GetImageName();
        GetStatType();
        GetAmount();
        return new PowerUp() { Item_Id = item_Id, Item_Name = item_Name, Game_Icon = icon, Item_Type = ItemType.Powerup, PowerUp_StatType = Model.PowerUp_StatType, PowerUp_Value = Model.PowerUp_Value };
    }

    private static void GetStatType()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stat Type");
        Model.PowerUp_StatType = (StatType)EditorGUILayout.Popup((int)Model.PowerUp_StatType, Enum.GetNames(typeof(StatType)));
        EditorGUILayout.EndHorizontal();
    }

    private static void GetAmount()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Stat Value");
        Model.PowerUp_Value = EditorGUILayout.IntField(Model.PowerUp_Value);
        EditorGUILayout.EndHorizontal();
    }


}
