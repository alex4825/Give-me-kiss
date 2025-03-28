using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CardManager : SingletonPersistent<CardManager>
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private PartnerCard _cardPrefab;

    private List<Partner> _partners;

    public List<PartnerCard> Cards { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        _partners = PersonManager.Instance.Partners;
        CreateCards();
    }

    private void CreateCards()
    {
        Cards = new List<PartnerCard>(_partners.Count);

        foreach (Partner partner in _partners)
        {
            PartnerCard card = Instantiate(_cardPrefab, _cardContainer);
            card.Initiate(partner);
            Cards.Add(card);
        }
    }
}
