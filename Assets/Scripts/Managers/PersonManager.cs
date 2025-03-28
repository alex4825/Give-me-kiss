using System;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : SingletonPersistent<PersonManager>
{
    public Player Player { get; private set; }
    public Partner CurrentPartner { get; private set; }
    public List<Partner> Partners { get; private set; }

    public event Action OnCurrentPartnerBlocked;

    protected override void Awake()
    {
        base.Awake();

        InitiatePlayerFromDataFile();
        InitiatePartnersFromDataFile();

        PartnerCard.OnCardClicked += SetCurrentPartner;
        Messanger.OnBackButtonClicked += ResetCurrentPartner;
        Messanger.OnEmotionShown += ChangeCurrentPersonsProgress;
        MainMenu.OnPlayButtonClicked += ResetCurrentPartner;
    }

    private void BlockPartner(Partner partner)
    {
        //if (partner.OriginName == CurrentPartner.OriginName)
        {
            CurrentPartner = null;
            OnCurrentPartnerBlocked?.Invoke();
        }
    }

    private void ChangeCurrentPersonsProgress(Emotion emotion)
    {
        Player.AddProgressFrom(emotion);
        CurrentPartner.AddProgressFrom(emotion);
    }

    private void SetCurrentPartner(Partner partner)
    {
        CurrentPartner = partner;
        Partner.OnNoAvailable += BlockPartner;
        //Partner.OnPresentKiss += KissBy;
    }

    /*private void KissBy(Partner partner)
    {
        OnCurrentPartnerKissed?.Invoke();
    }*/

    private void ResetCurrentPartner()
    {
        CurrentPartner = null;
    }

    private void InitiatePlayerFromDataFile()
    {
        PlayerData playerData = FileManager.Instance.JsonToPlayerData();

        Player = new Player(playerData);

        Debug.Log($"Player {Player.OriginName} initiated");
    }

    private void InitiatePartnersFromDataFile()
    {
        Partners = new List<Partner>();
        List<PartnerData> partnersData = FileManager.Instance.JsonToPartnerDataList();

        foreach (var partnerData in partnersData)
        {
            Partners.Add(new Partner(partnerData));
        }

        DebugInformer.ShowStringFrom<Partner>(Partners);
    }
}
