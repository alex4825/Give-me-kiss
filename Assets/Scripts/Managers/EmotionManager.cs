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

        Debug.Log("Эмоции:" + GetEmotionsInString());
    }


    private void InitiateAmotions()
    {
        Emotions = new List<Emotion>();
        List<EmotionData> emotionsData = FileManager.Instance.JsonToEmotionDataList();

        foreach (var emotionData in emotionsData)
        {
            Emotions.Add(new Emotion(emotionData));
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
