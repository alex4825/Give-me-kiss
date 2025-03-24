using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System;

public class Chat
{
    private string _filePath;
    private string ChatFilePostfix = "_chat.json";

    public Chat(Partner partner)
    {
        _filePath = Path.Combine(Application.persistentDataPath, partner.OriginName + ChatFilePostfix);

        InitiateHistory();
    }

    public List<AiToolbox.Message> History { get; private set; }
    public bool IsInitialized => History.Count > 0;

    private void InitiateHistory()
    {
        History = LoadHistory();
    }

    public void Add(AiToolbox.Message message)
    {
        History.Add(message);
        SaveHistory();
    }

    private List<AiToolbox.Message> LoadHistory()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            ChatHistoryWrapper wrapper = JsonConvert.DeserializeObject<ChatHistoryWrapper>(json);
            return wrapper.chatHistory ?? new List<AiToolbox.Message>();
        }
        else
        {
            History = new();
            SaveHistory(); //create new file
            return new List<AiToolbox.Message>();
        }
    }

    private void SaveHistory()
    {
        ChatHistoryWrapper wrapper = new ChatHistoryWrapper(History);
        string json = JsonConvert.SerializeObject(wrapper, Formatting.Indented);
        File.WriteAllText(_filePath, json);

        if (History.Count == 0)
        {
            Debug.LogWarning($"File {_filePath} doesn't exist. It was created now.");
        }
    }

    public void Clear()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
            Debug.Log($"File deleted: {_filePath}");
        }
        else
        {
            Debug.LogWarning($"File {_filePath} doesn't exist.");
        }

        History.Clear();
        InitiateHistory();
    }
}

[Serializable]
public class ChatHistoryWrapper
{
    [JsonProperty("chatHistory")]
    public List<AiToolbox.Message> chatHistory;

    public ChatHistoryWrapper(List<AiToolbox.Message> history)
    {
        chatHistory = history;
    }
}