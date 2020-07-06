using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Bundle : Item
{
    [JsonProperty("asset", Order = 4)]
    public string Bundle_Asset;
    [JsonProperty("price", Order = 5)]
    public string Bundle_Price;
    [JsonProperty("items", Order = 6)]
    public List<ItemReference> Bundle_Items;
}

public class ItemReference
{
    [JsonProperty("ref")]
    public int Reference;
    [JsonProperty("amount")]
    public int Amount;
}
