using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class EmotionManager : MonoBehaviour
{
    [SerializeField] private string _emotionsResourcesFile;

    public List<Emotion> Emotions { get; private set; }

    public static EmotionManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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
