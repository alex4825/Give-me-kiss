using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using System.Linq;

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

    public Emotion GetEmotionBy(string originName)
    {
        return Emotions.First(emotion =>  emotion.OriginName == originName);
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
