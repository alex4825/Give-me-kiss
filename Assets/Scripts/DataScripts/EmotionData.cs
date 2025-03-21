using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EmotionData
{
    [JsonProperty("name")]
    private string _name;

    [JsonProperty("strength")]
    private float _strength;

    public string Name => _name;

    public float Strength => _strength;
}
