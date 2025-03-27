using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartnerData : MonoBehaviour
{
    [JsonProperty("name")]
    private string _name;
    
    [JsonProperty("age")]
    private int _age;

    [JsonProperty("height")]
    private int _height;

    [JsonProperty("shortAboutSelf")]
    private string _shortAboutSelf;

    [JsonProperty("aboutSelf")]
    private string _aboutSelf;

    public string Name => _name;
    public int Age => _age;
    public int Height => _height;
    public string ShortAboutSelf => _shortAboutSelf;
    public string AboutSelf => _aboutSelf;

}
