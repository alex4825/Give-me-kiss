using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : SingletonPersistent<CardManager>
{
    [SerializeField] private Transform _cardContainer;
    [SerializeField] private MenuCard _cardPrefab;

    private List<Partner> _partners;

    public List<MenuCard> Cards { get; private set; }

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
        MenuCard partnerCard = Cards.First(card => card.Partner.OriginName == partner.OriginName);
        partnerCard.UpdateToKiss();
    }

    private void BlockCardWithPartner(Partner partner)
    {
        MenuCard partnerCard = Cards.First(card => card.Partner.OriginName == partner.OriginName);
        partnerCard.Block();
    }

    //вот тут вторая отвествтенность нарисовалась - спавн
    //такое тоже лучше выносить в фабрики, можешь почитать там есть нескольо паттернов - фабричный метод, абстрактная фабрика
    //и ещё чёт)
    private void CreateCards()
    {
        Cards = new List<MenuCard>(_partners.Count);

        foreach (Partner partner in _partners)
        {
            MenuCard card = Instantiate(_cardPrefab, _cardContainer);
            card.Initiate(partner);
            Cards.Add(card);
        }
    }
}
