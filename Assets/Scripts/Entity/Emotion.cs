using System;
using UnityEngine;

[Serializable]
public class Emotion
{
    public Emotion(EmotionResourcesData emotionData)
    {
        OriginName = emotionData.OriginName;
        Name = emotionData.Name;
        Strength = emotionData.Strength;
        IsPositive = emotionData.IsPositive;

        EmojiSprite = ResourcesFileLoader.LoadEmojiSpriteBy(OriginName);
    }

    public string OriginName { get; private set; }
    public string Name { get; private set; }
    public float Strength { get; set; }
    public bool IsPositive { get; private set; }
    public Sprite EmojiSprite { get; private set; }
}
