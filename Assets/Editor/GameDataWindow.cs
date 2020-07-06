using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class GameDataWindow : EditorWindow
{
    private Data MyData = new Data();
    private int Tab_Editor;

    #region Create New Item
    private Item CreateItem = new Item();
    private int Create_ID;
    private ItemType Create_Type;
    private string Create_Name;
    private Texture2D Create_Texture;
    private string Create_TextureName;
    #endregion

    #region Edit Item
    private Item EditItem = null;


    #endregion

    [MenuItem("Window/GameDataWindow")]
    static void OpenWindow()
    {
        GameDataWindow window = (GameDataWindow)GetWindow(typeof(GameDataWindow));
        window.Show();
    }

    private void OnGUI()
    {
        Tab_Editor = GUILayout.Toolbar(Tab_Editor, new string[] { "Create", "View", "Edit" });

        EditorGUILayout.Space(30);
        switch (Tab_Editor)
        {
            case 0:
                GUI_CreateNew();
                break;
            case 1:
                GUI_ViewExisting();
                break;
            case 2:
                GUI_Edit();
                break;
        }
    }

    private void GUI_CreateNew()
    {
        EditItem = null;

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item ID");
        GUILayout.Label(Create_ID.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Name");
        Create_Name = EditorGUILayout.TextField("Name");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item Type");
        Create_Type = (ItemType)EditorGUILayout.Popup((int)Create_Type, Enum.GetNames(typeof(ItemType)));
        EditorGUILayout.EndHorizontal();

        Create_ID = GenerateIdOnDropDownChange();
        switch (Create_Type)
        {
            case ItemType.Spaceship:
                Create_TextureName = GetImageName();


                Ship ship = new Ship() { Game_Icon = Create_TextureName, Item_Id = Create_ID, Item_Name = Create_Name, Item_Type = ItemType.Spaceship, Stats = new Dictionary<StatType, int>() };
                CreateItem = ship;
                break;
            case ItemType.Powerup:
                break;
            case ItemType.Currency:
                break;
            case ItemType.Bundle:
                break;
            default:
                break;
        }



        if (GUILayout.Button("Add"))
        {
            MyData.Items.Add(CreateItem);
            MyData.Save();
        }
    }

    private int GenerateIdOnDropDownChange()
    {
        int id = MyData.Items.Where(x => x.Item_Type == Create_Type).Count() + 1;
        string formattedId = String.Format("{0:000}", id);
        string fullID = string.Format("{0}{1}", (int)Create_Type + 1, formattedId);
        int result;
        if (int.TryParse(fullID, out result))
        {
            return result;
        }
        return -1;
    }

    private string GetImageName()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("GameItem Icon");
        GUILayout.BeginHorizontal("box");
        GUILayout.FlexibleSpace();
        Create_Texture = (Texture2D)EditorGUILayout.ObjectField(Create_Texture, typeof(Texture2D), false, GUILayout.Width(70), GUILayout.Height(70));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        if (Create_Texture != null)
            return Path.GetFileName(AssetDatabase.GetAssetPath(Create_Texture.GetInstanceID()));
        else
            return string.Empty;
    }

    private void OnEnable()
    {
        MyData.Pull();
    }

    private void GUI_ViewExisting()
    {
        EditItem = null;
        for (int i = 0; i < MyData.Items.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            GUILayout.Label(MyData.Items[i].Item_Name);
            if (GUILayout.Button("Edit"))
            {
                Tab_Editor = 2;
                GUI_Edit(MyData.Items[i]);
            }

            if (GUILayout.Button("Delete"))
            {
                MyData.Items.Remove(MyData.Items[i]);
                MyData.Save();
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private void GUI_Edit(Item item = null)
    {
   

    }
}

