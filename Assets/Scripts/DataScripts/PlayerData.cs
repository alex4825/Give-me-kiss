using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    [JsonProperty("originName")]
    public string OriginName { get; private set; }

    [JsonProperty("name")]
    public string Name { get; private set; }
}
