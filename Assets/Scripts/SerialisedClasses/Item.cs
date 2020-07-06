using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Item 
{
    [JsonProperty("id")]
    public int Item_Id;

    [JsonProperty("name")]
    public string Item_Name;

    [JsonProperty("type")]
    public ItemType Item_Type;

}
