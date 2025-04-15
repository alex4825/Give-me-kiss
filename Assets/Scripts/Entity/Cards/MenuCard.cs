using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuCard : Card
{
    [SerializeField] private TextMeshProUGUI _characterDescription;
    [SerializeField] private ButtonHandler _thisButtonHandler;
    [SerializeField] private Image _grayLayer;

    //статик ивенты не лучшая затея
    //лучше уж иметь список всех карт и подписываться на обычный ивент
    public static event Action<Partner> OnCardClicked;

    public Partner Partner => Person as Partner;

    private void OnEnable()
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

        if (Partner.IsAvailable == false)
        {
            Block();
        }

        if (Partner.IsConquered == true)
        {
            UpdateToKiss();

            //if (!PlayerPrefs.HasKey("FirstLaunch"))
            //{
            //    UpdateToKiss();

            //    PlayerPrefs.SetInt("FirstLaunch", 1);
            //    PlayerPrefs.Save();
            //}
        }

    }

    public void Block()
    {
        _grayLayer.gameObject.SetActive(true);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void UpdateToKiss()
    {
        PersonImage.sprite = Partner.KissSprite;
    }
}
