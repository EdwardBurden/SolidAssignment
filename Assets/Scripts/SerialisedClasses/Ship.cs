using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Ship : GameItem
{
    [JsonProperty("stats", Order = 5)]
    public Dictionary<StatType, int> Stats;
}

