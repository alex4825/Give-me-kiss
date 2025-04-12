using System;
using System.IO;
using UnityEngine;

public class Player : Person
{
    private PlayerPersistentData _persistentData;

    public Player(PlayerResourcesData playerData) : base(playerData)
    {
        InitiatePersistentData();
    }

    public int CharismaLevel { get; private set; }

    public event Action<int> OnCharismaLevelUp;
    public event Action<int> OnCharismaLevelDown;

    public override void AddProgressFrom(Emotion emotion)
    {
        base.AddProgressFrom(emotion);

        if (Progress >= MaxProgressValue)
        {
            CharismaLevel++;

            if (CharismaLevel == 0)
                CharismaLevel = 1;

            Progress = 0;

            OnCharismaLevelUp?.Invoke(CharismaLevel);

            Debug.Log($"Уровень харизмы повышен до {CharismaLevel}");
        }
        else if (Progress <= MinProgressValue)
        {
            CharismaLevel--;

            if (CharismaLevel == 0)
                CharismaLevel = -1;

            Progress = 0;

            OnCharismaLevelDown?.Invoke(CharismaLevel);

            Debug.Log($"Уровень харизмы понижен до {CharismaLevel}");
        }

        UpdatePersistentData();
    }


    private void InitiatePersistentData()
    {
        if (PersistantDataController.IsFileExists(OriginName))
        {
            _persistentData = PersistantDataController.Load<PlayerPersistentData>(OriginName);
        }
        else
        {
            _persistentData = new PlayerPersistentData(0, 1);
            PersistantDataController.Save(OriginName, _persistentData);
        }

        Progress = _persistentData.Progress;
        CharismaLevel = _persistentData.CharizmaLevel;
    }

    private void UpdatePersistentData()
    {
        _persistentData = new PlayerPersistentData(Progress, CharismaLevel);
        PersistantDataController.Save(OriginName, _persistentData);
    }
}
