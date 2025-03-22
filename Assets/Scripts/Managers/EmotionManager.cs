using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class EmotionManager : SingletonPersistent<EmotionManager>
{
    public List<Emotion> Emotions { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InitiateAmotions();
        DebugInformer.ShowStringFrom<Emotion>(Emotions);
    }


    private void InitiateAmotions()
    {
        Emotions = new List<Emotion>();
        Dictionary<string, EmotionData> emotionsData = FileManager.Instance.JsonToEmotionDataDictionary();

        foreach (var pair in emotionsData)
        {
            EmotionData emotionData = pair.Value;
            string emotionOriginName = pair.Key;

            Emotions.Add(new Emotion(emotionOriginName, emotionData));
        }
    }

    public string GetEmotionsInString()
    {
        string emotionsString = string.Empty;

        foreach (var emotion in Emotions)
        {
            emotionsString += emotion.OriginName + " ";
        }

        return emotionsString;
    }
}
