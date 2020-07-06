using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Data
{

    public List<Item> Items { get; private set; }

    public void Pull()
    {
        Items = DataHandler.Import();
    }

    public void Save()
    {
        DataHandler.Export(Items);
    }

}
