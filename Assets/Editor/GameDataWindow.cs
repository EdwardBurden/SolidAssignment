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

    #region 
    private Item CreateItem = new Item() { Item_Type = ItemType.Spaceship };
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
        /*   switch (Tab_Editor)
           {
               case 0:
                   EditItem = null;
                   GUI_Create_Edit();
                   Add();
                   break;
               case 1:
                   EditItem = null;
                   GUI_ViewExisting();
                   break;
               case 2:
                   GUI_Edit();
                   break;
           }*/
     
    }

    private void GUI_Create_Edit()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item ID");
        GUILayout.Label(CreateItem.Item_Id.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Name");
        CreateItem.Item_Name = EditorGUILayout.TextField("");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item Type");
        CreateItem.Item_Type = (ItemType)EditorGUILayout.Popup((int)CreateItem.Item_Type, Enum.GetNames(typeof(ItemType)));
        EditorGUILayout.EndHorizontal();

        CreateItem.Item_Id = GenerateIdOnDropDownChange();
        switch (CreateItem.Item_Type)
        {
            case ItemType.Spaceship:
                Ship ship = SpaceShipEditor.CreateShip();
                ship.Item_Id = CreateItem.Item_Id;
                ship.Item_Type = CreateItem.Item_Type;
                ship.Item_Name = CreateItem.Item_Name;
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
    }

    private void Add()
    {
        if (GUILayout.Button("Add"))
        {
            MyData.Items.Add(CreateItem);
            MyData.Save();
        }
    }

    private int GenerateIdOnDropDownChange()
    {
        int id = MyData.Items.Where(x => x.Item_Type == CreateItem.Item_Type).Count() + 1;
        string formattedId = String.Format("{0:000}", id);
        string fullID = string.Format("{0}{1}", (int)CreateItem.Item_Type + 1, formattedId);
        int result;
        if (int.TryParse(fullID, out result))
        {
            return result;
        }
        return -1;
    }

    private void OnEnable()
    {
        MyData.Pull();
        MyData.Save();
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
        if (item != null)
        {
            GUI_Create_Edit();
        }

    }
}

