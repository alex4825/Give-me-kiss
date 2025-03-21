using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Emotion", menuName = "SOs/Emotion")]
public class EmotionSO : ScriptableObject
{
    public string _name;
    public float _stranght;
    public Sprite _emojiSprite;
}
