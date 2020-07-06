using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Data
{
    [JsonProperty("items")]
    public List<Item> Items;
}
