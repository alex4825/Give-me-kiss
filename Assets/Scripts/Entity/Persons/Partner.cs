using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Partner : Person
{
    private string _fileIsOpenedPath;
    private string IsAvailableFilePostfix = "_isAvailable.json";

    public Partner(PartnerData partnerData) : base(partnerData as PersonData)
    {
        Age = partnerData.Age;
        Height = partnerData.Height;
        AboutSelf = partnerData.AboutSelf;
        ShortAboutSelf = partnerData.ShortAboutSelf;
        
        Chat = new Chat(this);

        _fileIsOpenedPath = Path.Combine(Application.persistentDataPath, OriginName + IsAvailableFilePostfix);
        IsAvailable = LoadIsAvailableFromFile();
    }

    public int Age { get; private set; }
    public int Height { get; private set; }
    public string ShortAboutSelf { get; private set; }
    public string AboutSelf { get; private set; }
    public Chat Chat { get; private set; }
    public bool IsAvailable { get; private set; }

    public event Action OnNoAvailable;

    public override void AddProgressFrom(Emotion emotion)
    {
        base.AddProgressFrom(emotion);

        if (Progress <= MinProgressValue)
        {
            IsAvailable = false;
            OnNoAvailable?.Invoke();
            Debug.Log($"Девушка {OriginName} более недоступна");
        }

        SaveIsAvailableToFile();
    }

    private bool LoadIsAvailableFromFile()
    {
        if (File.Exists(_fileIsOpenedPath))
        {
            string json = File.ReadAllText(_fileIsOpenedPath);
            bool charismaLevelValue = bool.Parse(json);
            return charismaLevelValue;
        }
        else
        {
            IsAvailable = true;
            SaveIsAvailableToFile();
        }

        return true;
    }

    private void SaveIsAvailableToFile()
    {
        File.WriteAllText(_fileIsOpenedPath, IsAvailable.ToString());
    }

    public override string ToString()
    {
        return $"Имя: {Name}, Возраст: {Age}, Рост: {Height}, Кратко о себе: {ShortAboutSelf}, Описание персонажа: {AboutSelf}";
    }

    /// <summary>
    /// ////////////////////////////////////////////////
    /// </summary>
    public override void ResetFiles()
    {
        base.ResetFiles();

        IsAvailable = true;
        File.Delete(_fileIsOpenedPath);
    }
}
