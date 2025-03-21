using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    [SerializeField] Image _personIcon;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _date;

    public void InitiateMessage(Player player, string message)
    {
        _personIcon.sprite = player.FaceSprite;
        _text.text = message;
        _date.text = DateTime.Now.ToString("yyyy.MM.dd HH:mm");
    }
}
