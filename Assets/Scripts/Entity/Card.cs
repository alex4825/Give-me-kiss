using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _characterDescription;
    [SerializeField] private ButtonHandler _thisButtonHandler;

    private Partner _partner;

    public static event Action<Partner> OnCardClicked;

    private void Awake()
    {
        _thisButtonHandler.OnClick += Card_OnCardClicked;
    }

    private void Card_OnCardClicked()
    {
        OnCardClicked?.Invoke(_partner);
    }

    public void Initiate(Partner partner)
    {
        _partner = partner;
        _characterImage.sprite = _partner.FaceSprite;
        _characterDescription.text = $"{_partner.Name}, {_partner.Age} {StringResolver.GetYearSuffix(_partner.Age)}. {_partner.ShortAboutSelf}";
    }
}
