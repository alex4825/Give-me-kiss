using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageObject : MonoBehaviour
{
    [SerializeField] Image _personIcon;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _date;
    [SerializeField] Image _backgroundImage;
    [SerializeField] Color _playerBackgroundMessageColor;
    [SerializeField] Color _partnerBackgroundMessageColor;

    public void InitiateMessageFor(Person person, AiToolbox.Message message)
    {
        _personIcon.sprite = person.FaceSprite;
        _text.text = message.text;
        _date.text = message.date; //DateTime.Now.ToString("yyyy.MM.dd HH:mm")

        if (person is Player)
        {
            _text.alignment = TextAlignmentOptions.BottomRight;
            _backgroundImage.color = _playerBackgroundMessageColor;
        }
        else if (person is Partner)
        {
            _text.alignment = TextAlignmentOptions.BottomLeft;
            _backgroundImage.color = _partnerBackgroundMessageColor;
        }
    }
}
