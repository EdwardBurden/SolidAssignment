using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Ship : GameItem
{
    [JsonProperty("stats")]
    public Dictionary<StatType, int> Stats;
}

