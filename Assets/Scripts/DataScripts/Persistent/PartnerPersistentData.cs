using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class PartnerPersistentData : PersonPersistentData
{
    [JsonProperty("chatHistory")]
    public List<AiToolbox.Message> ChatHistory;

    public PartnerPersistentData(float progress, List<AiToolbox.Message> chatHistory) : base(progress)
    {
        ChatHistory = chatHistory ?? new List<AiToolbox.Message>();
    }
}
