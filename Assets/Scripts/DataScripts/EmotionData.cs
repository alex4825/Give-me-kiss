using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EmotionData
{
    [JsonProperty("originName")]
    private string _originName;

    [JsonProperty("name")]
    private string _name;

    [JsonProperty("strength")]
    private float _strength;

    [JsonProperty("isPositive")]
    private bool _isPositive;

    public string OriginName => _originName;
    public string Name => _name;
    public float Strength => _strength;
    public bool IsPositive => _isPositive;
}
