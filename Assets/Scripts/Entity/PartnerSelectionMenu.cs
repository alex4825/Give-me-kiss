using System;
using UnityEngine;

public class PartnerSelectionMenu : MonoBehaviour
{
    [SerializeField] private ButtonHandler _backButton;

    public static event Action OnBackButtonClicked;

    private void Awake()
    {
        _backButton.OnClick += BackButton_OnClick;
    }

    private void BackButton_OnClick()
    {
        OnBackButtonClicked?.Invoke();
    }
}
