using UnityEngine;

public class MessangerCardsController : MonoBehaviour
{
    [SerializeField] private Card _playerCard;
    [SerializeField] private Card _partnerCard;
    [SerializeField] private EmotionObject _emotionPrefab;
    [SerializeField] private CharismaObject _charismaPrefab;

    //на Awake лучше в принципе ничего не добавлять, вызовы Awake слишком грузят систему
    private void Awake()
    {
        //можно отписаться от ивентов на OnDestroy например
        Messanger.OnEmotionShown += ShowCurrentPartnerAmotion;
        PersonManager.Instance.Player.OnCharismaLevelUp += ShowPlayerCharizmaLevelUp; 
        PersonManager.Instance.Player.OnCharismaLevelDown += ShowPlayerCharizmaLevelDown;
        Partner.OnPresentKiss += UpdateMessangerPartnerCardToKiss;
    }

    private void OnEnable()
    {
        _playerCard.Initiate(PersonManager.Instance.Player);
        _partnerCard.Initiate(PersonManager.Instance.CurrentPartner);
    }

    private void UpdateMessangerPartnerCardToKiss(Partner partner)
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

    private void ShowCurrentPartnerAmotion(Emotion emotion)
    {
        EmotionObject emotionObject = Instantiate(_emotionPrefab, _partnerCard.PopupLocation);
        emotionObject.Initiiate(emotion);
    }


}
