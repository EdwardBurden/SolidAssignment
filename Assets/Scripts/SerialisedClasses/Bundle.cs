using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Bundle : Item
{
    [JsonProperty("asset")]
    public string Bundle_Asset;
    [JsonProperty("price")]
    public string Bundle_Price;
    [JsonProperty("items")]
    public List<ItemReference> Bundle_Items;
}

public class ItemReference
{
    [JsonProperty("ref")]
    public string Reference;
    [JsonProperty("amount")]
    public int Amount;
}
