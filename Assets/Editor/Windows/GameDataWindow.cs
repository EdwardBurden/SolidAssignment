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
    private int Tab_Editor = 1;

    #region 
    private Item ModelItem = new Item() { Item_Type = ItemType.Spaceship };
    private bool EditMode;
    #endregion

    [MenuItem("Window/GameDataWindow")]
    static void OpenWindow()
    {
        GameDataWindow window = (GameDataWindow)GetWindow(typeof(GameDataWindow));
        window.Show();
    }

    [MenuItem("CONTEXT/GameDataWindow/RESET", false, 0)]
    static void Init()
    {
        Debug.Log("here");
    }

    private void OnGUI()
    {
        Tab_Editor = GUILayout.Toolbar(Tab_Editor, new string[] { "Create", "View", "Edit" });

        EditorGUILayout.Space(30);
        switch (Tab_Editor)
        {
            case 0:
                if (ModelItem == null || EditMode)
                {
                    ModelItem = new Item() { Item_Type = ItemType.Spaceship };
                }
                GUI_Create();
                EditMode = false;
                break;
            case 1:
                EditMode = false;
                ModelItem = null;
                GUI_ViewExisting();
                break;
            case 2:
                if (!EditMode)
                    ModelItem = null;
                EditMode = true;
                GUI_Edit();
                break;
        }



        GUILayout.BeginArea(new Rect(position.width - 100, position.height - 50, 100, 100));
        if (GUILayout.Button("Refresh", GUILayout.Height(20)))
        {
            MyData.Pull();
        }

        if (GUILayout.Button("Nuke Data", GUILayout.Height(20)))
        {
            MyData.Items.Clear();
            MyData.Save();
        }
        GUILayout.EndArea();
    }

    private void GUI_Create()
    {
        ShowNameAndID();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item Type");
        ModelItem.Item_Type = (ItemType)EditorGUILayout.Popup((int)ModelItem.Item_Type, Enum.GetNames(typeof(ItemType)));
        EditorGUILayout.EndHorizontal();

        ModelItem.Item_Id = GenerateId();
        switch (ModelItem.Item_Type)
        {
            case ItemType.Spaceship:
                Ship ship = SpaceShipEditor.CreateShip(ModelItem.Item_Id, ModelItem.Item_Name);
                ModelItem = ship;
                break;
            case ItemType.Powerup:
                PowerUp powerUp = PowerUpEditor.CreatePowerUp(ModelItem.Item_Id, ModelItem.Item_Name);
                ModelItem = powerUp;
                break;
            case ItemType.Currency:
                Currency currency = CurrencyEditor.CreateCurrency(ModelItem.Item_Id, ModelItem.Item_Name);
                ModelItem = currency;
                break;
            case ItemType.Bundle:
                Bundle bundle = BundleEditor.CreateBundle(ModelItem.Item_Id, ModelItem.Item_Name, MyData.Items);
                ModelItem = bundle;
                break;
            default:
                break;
        }
        Add();
    }

    private void OnInspectorUpdate()
    {
        Repaint();
    }

    private void ShowNameAndID()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Item ID");
        GUILayout.Label(ModelItem.Item_Id.ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Item Name");
        ModelItem.Item_Name = EditorGUILayout.TextField(ModelItem.Item_Name); ;
        EditorGUILayout.EndHorizontal();
    }

    private void Save()
    {
        if (GUILayout.Button("Save"))
        {
            int changedItem = MyData.Items.FindIndex(x => x.Item_Id == ModelItem.Item_Id);
            MyData.Items[changedItem] = ModelItem;
            MyData.Save();

            //  ModelItem = new Item() { Item_Type = ItemType.Spaceship };
        }
    }


    private void Add()
    {
        if (GUILayout.Button("Add"))
        {
            MyData.Items.Add(ModelItem);
            MyData.Save();
            ModelItem = new Item() { Item_Type = ItemType.Spaceship };
            CleanEditors();
        }
    }

    private int GenerateId()
    {
        int number = 0;
        List<Item> itemTypes = MyData.Items.Where(x => x.Item_Type == ModelItem.Item_Type).ToList();
        if (itemTypes.Count > 0)
        {
            number = itemTypes.Max(x => x.Item_Id) + 1;
        }
        else
        {
            number = GenerateNewIdFromType();
        }
        Debug.Log(number);
        return number;
    }

    private int GenerateNewIdFromType()
    {
        int id = MyData.DataAvailable ? MyData.Items.Where(x => x.Item_Type == ModelItem.Item_Type).Count() + 1 : 1;
        string formattedId = String.Format("{0:000}", id);
        string fullID = string.Format("{0}{1}", (int)ModelItem.Item_Type + 1, formattedId);
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
        titleContent = new GUIContent("Game Data Window");
        CleanEditors();
    }

    private void GUI_ViewExisting()
    {
        if (MyData.DataAvailable)
        {
            for (int i = 0; i < MyData.Items.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                GUILayout.Label(MyData.Items[i].Item_Name);
                if (GUILayout.Button("Edit"))
                {
                    Tab_Editor = 2;
                    ModelItem = MyData.Items[i];
                    EditMode = true;
                    SetModel();
                }

                if (GUILayout.Button("Delete"))
                {
                    MyData.Items.Remove(MyData.Items[i]);
                    MyData.Save();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }

    private void GUI_Edit()
    {
        if (ModelItem != null)
        {
            ShowNameAndID();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Item Type");
            GUILayout.Label(ModelItem.Item_Type.ToString());
            EditorGUILayout.EndHorizontal();

            switch (ModelItem.Item_Type)
            {
                case ItemType.Spaceship:
                    Ship ship = SpaceShipEditor.CreateShip(ModelItem.Item_Id, ModelItem.Item_Name);
                    ModelItem = ship;
                    break;
                case ItemType.Powerup:
                    PowerUp powerUp = PowerUpEditor.CreatePowerUp(ModelItem.Item_Id, ModelItem.Item_Name);
                    ModelItem = powerUp;
                    break;
                case ItemType.Currency:
                    Currency currency = CurrencyEditor.CreateCurrency(ModelItem.Item_Id, ModelItem.Item_Name);
                    ModelItem = currency;
                    break;
                case ItemType.Bundle:
                    Bundle bundle = BundleEditor.CreateBundle(ModelItem.Item_Id, ModelItem.Item_Name, MyData.Items);
                    ModelItem = bundle;
                    break;
                default:
                    break;
            }
            Save();
        }

    }


    private void SetModel()
    {
        switch (ModelItem.Item_Type)
        {
            case ItemType.Spaceship:
                SpaceShipEditor.SetModel((Ship)ModelItem);
                break;
            case ItemType.Powerup:
                PowerUpEditor.SetModel((PowerUp)ModelItem);
                break;
            case ItemType.Currency:
                CurrencyEditor.SetModel((Currency)ModelItem);
                break;
            case ItemType.Bundle:
                BundleEditor.SetModel((Bundle)ModelItem);
                break;
            default:
                break;
        }

    }

    private void CleanEditors()
    {
        SpaceShipEditor.SetModel(new Ship());
        PowerUpEditor.SetModel(new PowerUp());
        CurrencyEditor.SetModel(new Currency());
        BundleEditor.SetModel(new Bundle());
    }


}

