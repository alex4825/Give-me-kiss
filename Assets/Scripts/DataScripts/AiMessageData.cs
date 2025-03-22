using Newtonsoft.Json;

[System.Serializable]
public class AiMessageData
{
    [JsonProperty("message")]
    private string _message;

    [JsonProperty("emotion")]
    private string _emotion;

    public string Message => _message;
    public string Emotion => _emotion;
}
