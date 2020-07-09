using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Data
{
    public string DataPath;
    public List<Item> Items { get; private set; }

    public bool DataAvailable
    {
        get { return Items != null && Items.Count > 0; }

    }

    public void Pull()
    {
        Items = DataHandler.Import(DataPath);
        if (Items == null)
            Items = new List<Item>();
    }

    public void Save()
    {
        DataHandler.Export(Items , DataPath);
        Pull();
    }


}
