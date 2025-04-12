using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmotionObject : MonoBehaviour
{
    [SerializeField] private Image _emotionImage;
    [SerializeField] private TextMeshProUGUI _emotionText;

    public void Initiiate(Emotion emotion)
    {
        _emotionImage.sprite = emotion.EmojiSprite;
        _emotionText.text = emotion.Name;
    }

}
