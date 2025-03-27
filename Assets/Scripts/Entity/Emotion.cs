using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Emotion
{
    private string _originName;
    private string _name;
    private float _strength;
    private bool _isPositive;
    private Sprite _emojiSprite;

    public Emotion(EmotionData emotionData)
    {
        _originName = emotionData.OriginName;
        _name = emotionData.Name;
        _strength = emotionData.Strength;
        _isPositive = emotionData.IsPositive;

        _emojiSprite = FileManager.Instance.LoadEmojiSpriteBy(_originName);
    }

    public string OriginName => _originName;
    public string Name => _name;
    public float Strength => _strength;
    public bool IsPositive => _isPositive;
}
