using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private Transform _partnerSelectionMenu;
    [SerializeField] private Transform _messengerMenu;
    [SerializeField] private Transform _mainMenu;

    public GameState CurrentGameState { get; private set; }

    public Player Player { get; private set; }

    public Partner CurrentPartner { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Player = new Player();
        CurrentPartner = null;
        UpdateGameModeFrom(GameState.MainMenu);

        Card.OnPartnerChoosen += Card_OnPartnerChoosen;
        Messanger.OnButtonBackPressed += Messanger_OnButtonBackPressed;
    }

    private void Messanger_OnButtonBackPressed()
    {
        CurrentPartner = null;
        UpdateGameModeFrom(GameState.ChoosingPartner);
    }

    private void Card_OnPartnerChoosen(Partner partner)
    {
        CurrentPartner = partner;
        UpdateGameModeFrom(GameState.Messenger);
    }

    private void UpdateGameModeFrom(GameState state)
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
    }

    private void OnDestroy()
    {
        Card.OnPartnerChoosen -= Card_OnPartnerChoosen;
    }
}
