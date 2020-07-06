using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GameItem : Item
{
    [JsonProperty("icon", Order = 4)]
    public string Game_Icon;
}
