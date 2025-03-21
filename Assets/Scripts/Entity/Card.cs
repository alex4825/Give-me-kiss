using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _characterDescription;

    private Partner _partner;

    public static event Action<Partner> OnPartnerChoosen;

    public void Initiate(Partner partner)
    {
        _partner = partner;
        _characterImage.sprite = _partner.FaceSprite;
        _characterDescription.text = $"Имя: {_partner.Name}\nВозраст: {_partner.Age}";
    }

    public void ToDialog()
    {
        OnPartnerChoosen?.Invoke(_partner);
    }
}
