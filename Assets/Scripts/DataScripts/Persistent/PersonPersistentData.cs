using Newtonsoft.Json;
using System;

[Serializable]
public abstract class PersonPersistentData
{
    [JsonProperty("progress")]
    public float Progress;

    public PersonPersistentData(float progress)
    {
        Progress = progress;
    }
}
