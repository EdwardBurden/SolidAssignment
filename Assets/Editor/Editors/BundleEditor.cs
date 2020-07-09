using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Globalization;

public class BundleEditor : GameItemEditor
{
    private static Bundle Model = new Bundle();
    private static List<Item> SelectableItems;
    private static float Price;

    public static void SetModel(Bundle model)
    {
        Model = model;
        SetIcon(Model.Bundle_Asset);
        Price = Model.Bundle_Price != null ? float.Parse(Model.Bundle_Price, NumberStyles.Currency, CultureInfo.GetCultureInfo("en-us")) : 0;
    }

    public static Bundle CreateBundle(int item_Id, string item_Name, List<Item> availableItems)
    {
        SelectableItems = availableItems.Where(x => x.Item_Id != Model.Item_Id).ToList();
        string asset = GetImageName();
        GetBundlePrice();
        GetBundleItems();
        return new Bundle() { Item_Id = item_Id, Item_Name = item_Name, Item_Type = ItemType.Bundle, Bundle_Asset = asset, Bundle_Price = Model.Bundle_Price, Bundle_Items = Model.Bundle_Items };
    }

    private static void GetBundlePrice()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Bundle Price in $", GUILayout.Width(200));
        Price = EditorGUILayout.FloatField(Price);
        Model.Bundle_Price = Price.ToString("C", CultureInfo.GetCultureInfo("en-us"));
        EditorGUILayout.EndHorizontal();
    }

    private static void GetBundleItems()
    {
        if (SelectableItems.Count > 0)
        {
            if (Model.Bundle_Items != null)
            {
                string[] nameArray = SelectableItems.Select(x => x.Item_Name).ToArray();
                for (int i = 0; i < Model.Bundle_Items.Count; i++)
                {
                    Item item = SelectableItems.FirstOrDefault(x => x.Item_Id == Model.Bundle_Items[i].Reference);
                    if (item != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Select Item", GUILayout.Width(200));
                        int itemIndex = SelectableItems.IndexOf(item);
                        itemIndex = EditorGUILayout.Popup(itemIndex, nameArray);
                        Model.Bundle_Items[i].Reference = SelectableItems[itemIndex].Item_Id;
                        Model.Bundle_Items[i].Amount = EditorGUILayout.IntField(Model.Bundle_Items[i].Amount);
                        if (GUILayout.Button("Remove"))
                        {
                            Model.Bundle_Items.RemoveAt(i);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        Model.Bundle_Items.RemoveAt(i);
                    }

                }
            }
            if (GUILayout.Button("Add Item"))
            {
                if (Model.Bundle_Items == null)
                    Model.Bundle_Items = new List<ItemReference>();
                Model.Bundle_Items.Add(new ItemReference() { Reference = SelectableItems[0].Item_Id, Amount = 1 });
            }

        }
    }
}
