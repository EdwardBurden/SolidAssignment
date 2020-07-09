using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
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
    [EnumMember(Value = "power")]
    Power,
    [EnumMember(Value = "speed")]
    Speed,
    [EnumMember(Value = "hull")]
    HullPoints
}

//add description attribute later
/*public enum GameDataTabs
{
    Create_New_Item,
    View_Existing
}
*/
