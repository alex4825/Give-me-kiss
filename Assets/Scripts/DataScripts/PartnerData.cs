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

    [JsonProperty("aboutSelf")]
    private string _aboutSelf;

    [JsonProperty("dialogData")]
    private string _dialogData;

    public string Name => _name;

    public int Age => _age;

    public int Height => _height;

    public string AboutSelf => _aboutSelf;

    public string DialogData => _dialogData;

}
