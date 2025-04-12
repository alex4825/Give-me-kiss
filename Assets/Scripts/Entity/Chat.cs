using System.Collections.Generic;

public class Chat
{
    public Chat()
    {
        History = new List<AiToolbox.Message>();
    }

    public List<AiToolbox.Message> History { get; private set; }

    public bool IsInitialized => History == null ? false : History.Count > 0;

    public void Add(AiToolbox.Message message)
    {
        History.Add(message);
    }

    public void SetHistory(List<AiToolbox.Message> history)
    {
        History = history ?? new List<AiToolbox.Message>();
    }

    public void Clear()
    {
        History.Clear();
    }
}