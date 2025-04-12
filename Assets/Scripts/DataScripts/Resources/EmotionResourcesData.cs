using Newtonsoft.Json;

[System.Serializable]
public class EmotionResourcesData
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
