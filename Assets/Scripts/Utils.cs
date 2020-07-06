using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

public static class Utils
{

}

[JsonConverter(typeof(StringEnumConverter))]
public enum ItemType
{
    Spaceship,
    Powerup,
    Currency,
    Bundle
}

[JsonConverter(typeof(StringEnumConverter))]
public enum StatType
{
    [JsonProperty("power")]
    power,
    [JsonProperty("speed")]
    speed
}
