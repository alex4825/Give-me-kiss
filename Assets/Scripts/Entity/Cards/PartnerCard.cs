using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartnerCard : Card
{
    [SerializeField] private TextMeshProUGUI _characterDescription;
    [SerializeField] private ButtonHandler _thisButtonHandler;

    public static event Action<Partner> OnCardClicked;

    private Partner Partner => Person as Partner;

    private void Awake()
    {
        _thisButtonHandler.OnClick += Card_OnCardClicked;
    }

    private void Card_OnCardClicked()
    {
        OnCardClicked?.Invoke(Partner);
    }

    public void Initiate(Partner partner)
    {
        base.Initiate(partner);

        List<Partner> initiatedPartners = PersonManager.Instance.Partners;
        Person = initiatedPartners.First(partner => partner.OriginName == Partner.OriginName);

        _characterDescription.text = $"{partner.Name}, {partner.Age} {StringResolver.GetYearSuffix(partner.Age)}. {partner.ShortAboutSelf}";
    }

}
