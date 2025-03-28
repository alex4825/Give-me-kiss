using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartnerData : PersonData
{
    [JsonProperty("age")]
    public int Age { get; private set; }

    [JsonProperty("height")]
    public int Height { get; private set; }

    [JsonProperty("shortAboutSelf")]
    public string ShortAboutSelf { get; private set; }

    [JsonProperty("aboutSelf")]
    public string AboutSelf { get; private set; }
}
