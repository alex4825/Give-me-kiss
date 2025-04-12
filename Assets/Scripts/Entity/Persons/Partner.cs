using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Partner : Person
{
    private PartnerPersistentData _persistentData;

    public Partner(PartnerResourcesData partnerData) : base(partnerData)
    {
        Age = partnerData.Age;
        Height = partnerData.Height;
        AboutSelf = partnerData.AboutSelf;
        ShortAboutSelf = partnerData.ShortAboutSelf;
        KissSprite = ResourcesFileLoader.LoadCharacterSpriteBy($"{OriginName}_kiss");

        Chat = new Chat();

        InitiatePersistentData();

        CheckProgress();
    }

    public int Age { get; private set; }
    public int Height { get; private set; }
    public string ShortAboutSelf { get; private set; }
    public string AboutSelf { get; private set; }
    public Sprite KissSprite { get; protected set; }
    public Chat Chat { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsConquered { get; private set; }

    public static event Action<Partner> OnNoAvailable;
    public static event Action<Partner> OnPresentKiss;

    public override void AddProgressFrom(Emotion emotion)
    {
        base.AddProgressFrom(emotion);

        CheckProgress();

        UpdatePersistentData();
    }

    private void CheckProgress()
    {
        if (Progress <= MinProgressValue)
        {
            IsAvailable = false;
            OnNoAvailable?.Invoke(this);
            Debug.Log($"Девушка {OriginName} более недоступна");
        }
        else if (Progress >= MaxProgressValue)
        {
            IsConquered = true;
            OnPresentKiss?.Invoke(this);
            Debug.Log($"Девушка {OriginName} подарила поцелуй!");
        }
        else
        {
            IsAvailable = true;
            IsConquered = false;
        }
    }

    public override string ToString()
    {
        return $"Имя: {Name}, Возраст: {Age}, Рост: {Height}, Кратко о себе: {ShortAboutSelf}, Описание персонажа: {AboutSelf}";
    }

    private void InitiatePersistentData()
    {
        if (PersistantDataController.IsFileExists(OriginName))
        {
            _persistentData = PersistantDataController.Load<PartnerPersistentData>(OriginName);
        }
        else
        {
            _persistentData = new PartnerPersistentData(0, null);
            PersistantDataController.Save(OriginName, _persistentData);
        }

        Progress = _persistentData.Progress;
        Chat.SetHistory(_persistentData.ChatHistory);
    }
    
    private void UpdatePersistentData()
    {
        _persistentData = new PartnerPersistentData(Progress, Chat.History);
        PersistantDataController.Save(OriginName, _persistentData);
    }
}
