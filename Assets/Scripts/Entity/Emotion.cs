using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Emotion
{
    private string _originName;
    private string _name;
    private float _stranght;
    private Sprite _emojiSprite;
    private const string ResourcesImagesFolder = "Images/Emoji/";

    public Emotion(string originName, EmotionData emotionData)
    {
        _originName = originName;
        _name = emotionData.Name;
        _stranght = emotionData.Strength;

        _emojiSprite = SpriteReceiver.GetFromFile(ResourcesImagesFolder + originName);
    }

    public override string ToString()
    {
        return $"{_originName}: {_name}, {_stranght}."; 
    }
}
