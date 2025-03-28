using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        Partner.OnNoAvailable += BlockCardWithPartner;
        Partner.OnPresentKiss += ChangeCardToKissFrom;
    }

    private void ChangeCardToKissFrom(Partner partner)
    {
        PartnerCard partnerCard = Cards.First(card => card.Partner.OriginName == partner.OriginName);
        partnerCard.UpdateToKiss();
    }

    private void BlockCardWithPartner(Partner partner)
    {
        PartnerCard partnerCard = Cards.First(card => card.Partner.OriginName == partner.OriginName);
        partnerCard.Block();
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
