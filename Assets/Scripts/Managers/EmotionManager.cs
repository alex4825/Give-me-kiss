using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class EmotionManager : SingletonPersistent<EmotionManager>
{
    [SerializeField] private string _emotionsResourcesFile;

    public List<Emotion> Emotions { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InitiateAmotionsFromDataFile(_emotionsResourcesFile);
        DebugInformer.ShowStringFrom<Emotion>(Emotions);
    }

    //public EmotionData GetDataOf(string emotionName) => Emotions[emotionName];

    private void InitiateAmotionsFromDataFile(string fileName)
    {
        Emotions = new List<Emotion>();
        Dictionary<string, EmotionData> emotionsData = JsonConverter.ToDictionary<EmotionData>(fileName);

        foreach (var pair in emotionsData)
        {
            EmotionData emotionData = pair.Value;
            string emotionOriginName = pair.Key;

            Emotions.Add(new Emotion(emotionOriginName, emotionData));
        }
    }
}
