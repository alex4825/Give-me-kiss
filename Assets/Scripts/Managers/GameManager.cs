using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    [SerializeField] private Transform _partnerSelectionMenu;
    [SerializeField] private Transform _messengerMenu;

    public GameState CurrentGameState { get; private set; }

    public Player Player { get; private set; }

    public Partner CurrentPartner { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Player = new Player();
        CurrentPartner = null;
        CurrentGameState = GameState.ChoosingPartner;
        _partnerSelectionMenu.gameObject.SetActive(true);

        Card.OnPartnerChoosen += Card_OnPartnerChoosen;
    }

    private void Card_OnPartnerChoosen(Partner partner)
    {
        CurrentPartner = partner;
        OpenMessengerWith(partner);
    }

    private void OpenMessengerWith(Partner partner)
    {
        CurrentGameState = GameState.Messenger;

        _partnerSelectionMenu.gameObject.SetActive(false);
        _messengerMenu.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        Card.OnPartnerChoosen -= Card_OnPartnerChoosen;
    }
}
