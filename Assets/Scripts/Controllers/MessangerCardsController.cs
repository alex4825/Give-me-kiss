using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessangerCardsController : MonoBehaviour
{
    [SerializeField] private Card _playerCard;
    [SerializeField] private Card _partnerCard;

    private void Awake()
    {
        Messanger.OnEmotionShown += Messanger_OnEmotionShown;
    }
    
    private void OnEnable()
    {
        _playerCard.Initiate(PersonManager.Instance.Player);
        _partnerCard.Initiate(PersonManager.Instance.CurrentPartner);
    }

    private void Messanger_OnEmotionShown(Emotion emotion)
    {
        
    }
}
