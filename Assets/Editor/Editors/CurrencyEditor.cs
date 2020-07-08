using UnityEditor;

public class CurrencyEditor
{
    private static Currency Model = new Currency();


    public static void SetModel(Currency model)
    {
        Model = model;
    }

    public static Currency CreateCurrency(int item_Id, string item_Name)
    {
        return new Currency() { Item_Id = item_Id, Item_Name = item_Name , Item_Type = ItemType.Currency };
    }
}
