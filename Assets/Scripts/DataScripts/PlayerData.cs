using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    [JsonProperty("originName")]
    private string _originName;
    
    [JsonProperty("name")]
    private string _name;

    public string OriginName => _originName;

    public string Name => _name;
}
