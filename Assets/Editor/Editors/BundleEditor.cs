using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BundleEditor : GameItemEditor
{
    private static Bundle Model = new Bundle();
    private static List<Item> SelectedableItems;

    public static void SetModel(Bundle model)
    {
        Model = model;
    }

    public static Bundle CreateBundle(int item_Id, string item_Name, List<Item> availableItems)
    {
        SelectedableItems = availableItems;
        string ImageName = GetImageName(); // move to statioc stuff later
        GetBundlePrice();
        GetBundleItems();
        return new Bundle() { Item_Id = item_Id, Item_Name = item_Name, Item_Type = ItemType.Bundle, Bundle_Asset = ImageName, Bundle_Price = Model.Bundle_Price, Bundle_Items = Model.Bundle_Items };
    }

    private static void GetBundlePrice()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bundle Price");
        Model.Bundle_Price = EditorGUILayout.TextField(Model.Bundle_Price); //make better later
        EditorGUILayout.EndHorizontal();
    }

    private static void GetBundleItems()
    {
        if (Model.Bundle_Items != null)
        {
            string[] nameArray = SelectedableItems.Select(x => x.Item_Name).ToArray();
            for (int i = 0; i < Model.Bundle_Items.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Select Item");

                Item item = SelectedableItems.FirstOrDefault(x => x.Item_Id == Model.Bundle_Items[i].Reference);
                int itemIndex = item != null ? SelectedableItems.IndexOf(item) : 0;


                itemIndex = EditorGUILayout.Popup(itemIndex, nameArray);

                Model.Bundle_Items[i].Reference = SelectedableItems[itemIndex].Item_Id;

                Model.Bundle_Items[i].Amount = EditorGUILayout.IntField(Model.Bundle_Items[i].Amount);
                EditorGUILayout.EndHorizontal();


            }
        }

        if (GUILayout.Button("Add Item"))
        {
            if (Model.Bundle_Items == null)
                Model.Bundle_Items = new List<ItemReference>();
            Model.Bundle_Items.Add(new ItemReference() { Reference = 0, Amount = 1 });
        }

    }
}
