using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmotionManager : SingletonPersistent<EmotionManager>
{
    public List<Emotion> Emotions { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        InitiateAmotions();

        Debug.Log("Эмоции:" + GetEmotionsInString());

        PersonManager.Instance.Player.OnCharismaLevelUp += Player_OnCharismaLevelChanged;
    }

    private void Player_OnCharismaLevelChanged(int newLevel)
    {
        UpdateEmotionsStrangthFrom(newLevel);
    }

    private void InitiateAmotions()
    {
        int currentPlayerCharismaLevel = PersonManager.Instance.Player.CharismaLevel;

        List<EmotionResourcesData> emotionsData = ResourcesFileLoader.JsonToEmotionDataList();

        Emotions = new List<Emotion>();

        for (int i = 0; i < emotionsData.Count; i++)
        {
            Emotions.Add(new Emotion(emotionsData[i]));
        }

        UpdateEmotionsStrangthFrom(currentPlayerCharismaLevel);
    }

    private void UpdateEmotionsStrangthFrom(int currentPlayerCharismaLevel)
    {
        for (int i = 0; i < Emotions.Count; i++)
        {
            if (currentPlayerCharismaLevel > 0 && Emotions[i].IsPositive)
                Emotions[i].Strength += currentPlayerCharismaLevel;
            else if (currentPlayerCharismaLevel < 0 && Emotions[i].IsPositive == false)
                Emotions[i].Strength += currentPlayerCharismaLevel;
        }
    }

    public Emotion GetEmotionBy(string originName)
    {
        Emotion emotion = Emotions.FirstOrDefault(emotion => emotion.OriginName == originName);

        if (emotion == null)
            return Emotions.First(emotion => emotion.OriginName == "Calm");
        else
            return emotion;
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
