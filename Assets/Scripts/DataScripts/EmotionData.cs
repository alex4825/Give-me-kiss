using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EmotionData
{
    [JsonProperty("originName")]
    public string OriginName { get; private set; }

    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("strength")]
    public float Strength { get; private set; }

    [JsonProperty("isPositive")]
    public bool IsPositive { get; private set; }
}
