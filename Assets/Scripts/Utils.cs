using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public static class Utils
{

}

public enum ItemType
{
    Spaceship,
    Powerup,
    Currency,
    Bundle
}

public enum StatType
{
    [JsonProperty("power")]
    power,
    [JsonProperty("speed")]
    speed
}
