using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class JsonConverter
{
    public static Dictionary<string, T> ToDictionary<T>(string fileName)
    {
        Dictionary<string, T> keyValuePairs = new Dictionary<string, T>();

        TextAsset jsonFile = Resources.Load<TextAsset>("JsonData/" + fileName);

        if (jsonFile != null)
        {
            keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/JsonData/{fileName}.json doesn't exist!");
        }

        return keyValuePairs;
    }

    public static T ToObject<T>(string fileName)
    {
        T result = default;

        TextAsset jsonFile = Resources.Load<TextAsset>("JsonData/" + fileName);

        if (jsonFile != null)
        {
            result = JsonConvert.DeserializeObject<T>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/JsonData/{fileName}.json doesn't exist!");
        }

        return result;
    }
}
