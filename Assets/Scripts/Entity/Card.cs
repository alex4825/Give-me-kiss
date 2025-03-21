using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _characterDescription;

    private Character _character;

    public void Initiate(Character character)
    {
        _character = character;
        _characterImage.sprite = _character.FaceSprite;
        _characterDescription.text = $"Имя: {_character.Name}\nВозраст: {_character.Age}";
    }
}
