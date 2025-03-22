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

    public Emotion(string originName, EmotionData emotionData)
    {
        _originName = originName;
        _name = emotionData.Name;
        _stranght = emotionData.Strength;

        _emojiSprite = FileManager.Instance.LoadEmojiSpriteBy(_originName);
    }

    public string OriginName => _originName;
    public string Name => _name;
}
