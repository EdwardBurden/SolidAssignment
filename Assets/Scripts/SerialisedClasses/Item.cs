using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class Item
{
    [JsonProperty("id" ,Order =1)]
    public int Item_Id;

    [JsonProperty("type", Order = 2)]
    public ItemType Item_Type;

    [JsonProperty("name", Order = 3)]
    public string Item_Name;
}

public class ItemConverter : JsonCreationConverter<Item>
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    protected override Item Create(Type objectType, JObject jObject)
    {
        if (FieldExists("type", jObject))
        {
            ItemType type = jObject["type"].ToObject<ItemType>();
            switch (type)
            {
                case ItemType.Spaceship:
                    return new Ship();
                case ItemType.Powerup:
                    return new PowerUp();
                case ItemType.Currency:
                    return new Currency();
                case ItemType.Bundle:
                    return new Bundle();
                default:
                    break;
            }

        }
        return null;
    }

    private bool FieldExists(string fieldName, JObject jObject)
    {
        return jObject[fieldName] != null;
    }
}

public abstract class JsonCreationConverter<T> : JsonConverter
{
    /// <summary>
    /// Create an instance of objectType, based properties in the JSON object
    /// </summary>
    /// <param name="objectType">type of object expected</param>
    /// <param name="jObject">
    /// contents of JSON object that will be deserialized
    /// </param>
    /// <returns></returns>
    protected abstract T Create(Type objectType, JObject jObject);

    public override bool CanConvert(Type objectType)
    {
        return typeof(T).IsAssignableFrom(objectType);
    }

    public override bool CanWrite
    {
        get { return false; }
    }

    public override object ReadJson(JsonReader reader,
                                    Type objectType,
                                     object existingValue,
                                     JsonSerializer serializer)
    {
        // Load JObject from stream
        JObject jObject = JObject.Load(reader);

        // Create target object based on JObject
        T target = Create(objectType, jObject);

        // Populate the object properties
        serializer.Populate(jObject.CreateReader(), target);

        return target;
    }
}