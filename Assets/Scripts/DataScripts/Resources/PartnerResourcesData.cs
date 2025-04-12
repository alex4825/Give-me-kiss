using Newtonsoft.Json;

[System.Serializable]
public class PartnerResourcesData : PersonResourcesData
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
