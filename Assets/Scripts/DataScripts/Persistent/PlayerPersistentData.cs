using Newtonsoft.Json;

public class PlayerPersistentData : PersonPersistentData
{
    [JsonProperty("charizmaLevel")]
    public int CharizmaLevel;

    public PlayerPersistentData(float progress, int charizmaLevel) : base(progress)
    {
        CharizmaLevel = charizmaLevel;
    }
}
