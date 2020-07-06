using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PowerUp : GameItem
{
    [JsonProperty("stat", Order = 5)]
    public StatType PowerUp_StatType;
    [JsonProperty("amount", Order = 6)]
    public int PowerUp_Value;
}
