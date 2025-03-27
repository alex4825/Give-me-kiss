using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CardManager : SingletonPersistent<CardManager>
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private Card _cardPrefab;

    private List<Partner> _partners;

    public List<Card> Cards { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _partners = PartnerManager.Instance.Partners;
        CreateCards();
    }

    private void CreateCards()
    {
        Cards = new List<Card>(_partners.Count);

        foreach (Partner partner in _partners)
        {
            Card card = Instantiate(_cardPrefab, _cardContainer);
            card.Initiate(partner);
        }
    }
}
