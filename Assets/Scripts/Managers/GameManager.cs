using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private Transform _partnerSelectionMenu;
    [SerializeField] private Transform _messengerMenu;
    [SerializeField] private Transform _mainMenu;

    public GameState CurrentGameState { get; private set; }

    public static event Action<GameState> OnGameStateChanged;

    protected override void Awake()
    {
        base.Awake();

        UpdateGameModeTo(GameState.MainMenu);

        PartnerCard.OnCardClicked += OpenMessenger;
        Messanger.OnBackButtonClicked += OpenChoosingPartnerMenu;
        MainMenu.OnPlayButtonClicked += OpenChoosingPartnerMenu;
        PartnerSelectionMenu.OnBackButtonClicked += OpenMainMenu;
    }

    private void OpenMainMenu()
    {
        UpdateGameModeTo(GameState.MainMenu);
    }

    private void OpenChoosingPartnerMenu()
    {
        UpdateGameModeTo(GameState.ChoosingPartner);
    }

    private void OpenMessenger(Partner partner)
    {
        UpdateGameModeTo(GameState.Messenger);
    }

    private void UpdateGameModeTo(GameState state)
    {
        CurrentGameState = state;

        _partnerSelectionMenu.gameObject.SetActive(false);
        _messengerMenu.gameObject.SetActive(false);
        _mainMenu.gameObject.SetActive(false);

        switch (state)
        {
            case GameState.Messenger:
                _messengerMenu.gameObject.SetActive(true);
                break;

            case GameState.ChoosingPartner:
                _partnerSelectionMenu.gameObject.SetActive(true);
                break;
            
            case GameState.MainMenu:
                _mainMenu.gameObject.SetActive(true);
                break;
        }

        OnGameStateChanged?.Invoke(state);
    }

}
