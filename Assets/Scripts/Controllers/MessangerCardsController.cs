using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessangerCardsController : MonoBehaviour
{
    [SerializeField] private Card _playerCard;
    [SerializeField] private Card _partnerCard;
    [SerializeField] private EmotionObject _emotionPrefab;
    [SerializeField] private CharismaObject _charismaPrefab;

    private void Awake()
    {
        Messanger.OnEmotionShown += ShowCurrentPartnerAmotion;
        PersonManager.Instance.Player.OnCharismaLevelUp += ShowPlayerCharizmaLevelUp; 
        PersonManager.Instance.Player.OnCharismaLevelDown += ShowPlayerCharizmaLevelDown;
        Partner.OnPresentKiss += UpdateMessangerPartnerCard;
    }

    private void UpdateMessangerPartnerCard(Partner partner)
    {
        _partnerCard.PersonImage.sprite = partner.KissSprite;
    }

    private void ShowPlayerCharizmaLevelDown(int level)
    {
        CharismaObject charismaObject = Instantiate(_charismaPrefab, _playerCard.PopupLocation);
        charismaObject.SetValue(false);
        _playerCard.UpdateProgress(0);
    }

    private void ShowPlayerCharizmaLevelUp(int level)
    {
        CharismaObject charismaObject = Instantiate(_charismaPrefab, _playerCard.PopupLocation);
        charismaObject.SetValue(true);
        _playerCard.UpdateProgress(0);
    }

    private void OnEnable()
    {
        _playerCard.Initiate(PersonManager.Instance.Player);
        _partnerCard.Initiate(PersonManager.Instance.CurrentPartner); 
    }

    private void ShowCurrentPartnerAmotion(Emotion emotion)
    {
        EmotionObject emotionObject = Instantiate(_emotionPrefab, _partnerCard.PopupLocation);
        emotionObject.SetFrom(emotion);
    }


}
