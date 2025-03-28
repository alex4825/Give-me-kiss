using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        PersonManager.Instance.OnCurrentPartnerBlocked += BlockCardWithCurrentPartner;
        Partner.OnPresentKiss += ChangeCardToKissFrom;
    }

    private void ChangeCardToKissFrom(Partner partner)
    {
        PartnerCard partnerCard = Cards.First(card => card.Partner.OriginName == partner.OriginName);
        partnerCard.UpdateToKiss();
    }

    private void BlockCardWithCurrentPartner()
    {
        PartnerCard partnerCard = Cards.First(card => card.Partner.OriginName == PersonManager.Instance.CurrentPartner.OriginName);
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
