using System;
using System.IO;
using UnityEngine;

[Serializable]
public class Partner : Person
{
    private string _fileIsAvailablePath;
    private string IsAvailableFilePostfix = "_isAvailable.json";

    private string _fileIsConqueredPath;
    private string IsConqueredFilePostfix = "_isConquered.json";

    public Partner(PartnerData partnerData) : base(partnerData as PersonData)
    {
        Age = partnerData.Age;
        Height = partnerData.Height;
        AboutSelf = partnerData.AboutSelf;
        ShortAboutSelf = partnerData.ShortAboutSelf;

        Chat = new Chat(this);

        _fileIsAvailablePath = Path.Combine(Application.persistentDataPath, OriginName + IsAvailableFilePostfix);
        _fileIsConqueredPath = Path.Combine(Application.persistentDataPath, OriginName + IsConqueredFilePostfix);

        IsAvailable = LoadIsAvailableFromFile();
        IsConquered = LoadIsConqueredFromFile();

        if (IsConquered)
            OnPresentKiss?.Invoke(this);
    }

    public int Age { get; private set; }
    public int Height { get; private set; }
    public string ShortAboutSelf { get; private set; }
    public string AboutSelf { get; private set; }
    public Chat Chat { get; private set; }
    public bool IsAvailable { get; private set; }
    public bool IsConquered { get; private set; } = false;

    public static event Action<Partner> OnNoAvailable;
    public static event Action<Partner> OnPresentKiss;

    public override void AddProgressFrom(Emotion emotion)
    {
        base.AddProgressFrom(emotion);

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

        SaveIsAvailableToFile();
        SaveIsConquredToFile();
    }

    private bool LoadIsAvailableFromFile()
    {
        if (File.Exists(_fileIsAvailablePath))
        {
            string json = File.ReadAllText(_fileIsAvailablePath);
            bool isAvailableLevelValue = bool.Parse(json);
            return isAvailableLevelValue;
        }
        else
        {
            IsAvailable = true;
            SaveIsAvailableToFile();
        }

        return true;
    }

    private bool LoadIsConqueredFromFile()
    {
        if (File.Exists(_fileIsConqueredPath))
        {
            string json = File.ReadAllText(_fileIsConqueredPath);
            bool isConquredLevelValue = bool.Parse(json);
            return isConquredLevelValue;
        }
        else
        {
            IsConquered = false;
            SaveIsConquredToFile();
        }

        return true;
    }

    private void SaveIsAvailableToFile()
    {
        File.WriteAllText(_fileIsAvailablePath, IsAvailable.ToString());
    }

    private void SaveIsConquredToFile()
    {
        File.WriteAllText(_fileIsConqueredPath, IsConquered.ToString());
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
        File.Delete(_fileIsAvailablePath);
    }
}
