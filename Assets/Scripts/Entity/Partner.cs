using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Partner : Person
{
    private string _filePath;
    private string SympathyFilePostfix = "_sympathy.json";
    private float _maxSimpathyValue = 100;
    private float _minSimpathyValue;

    public Partner(PartnerData partnerData)
    {
        OriginName = partnerData.OriginName;
        Name = partnerData.Name;
        Age = partnerData.Age;
        Height = partnerData.Height;
        AboutSelf = partnerData.AboutSelf;
        ShortAboutSelf = partnerData.ShortAboutSelf;
        FaceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_face");
        AppearanceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_full");
        BasicColor = partnerData.BasicColor;
        Chat = new Chat(this);

        _filePath = Path.Combine(Application.persistentDataPath, OriginName + SympathyFilePostfix);

        Sympathy = LoadSympathyFromFile();

        _minSimpathyValue = -_maxSimpathyValue / 2;
    }

    public int Age { get; private set; }
    public int Height { get; private set; }
    public string ShortAboutSelf { get; private set; }
    public string AboutSelf { get; private set; }
    public Chat Chat { get; private set; }
    public Color BasicColor { get; private set; }
    public float Sympathy { get; private set; }
    public float SympathyNormalized
    {
        get
        {
            if (Sympathy >= 0)
                return Sympathy / _maxSimpathyValue;
            else
                return Sympathy / -_minSimpathyValue;
        }
    }

    public void AddSympathyFrom(Emotion emotion)
    {
        Sympathy += emotion.Strength;

        if (Sympathy > _maxSimpathyValue)
        {
            Sympathy = _maxSimpathyValue;
        }
        else if (Sympathy < _minSimpathyValue)
        {
            Sympathy = _minSimpathyValue;
        }

        SaveSympathyToFile();
        Debug.Log($"Added {emotion.OriginName} with strangth {emotion.Strength} for {OriginName}");
    }

    private float LoadSympathyFromFile()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            float sympathyValue = float.Parse(json);
            return sympathyValue;
        }

        return 0;
    }

    private void SaveSympathyToFile()
    {
        File.WriteAllText(_filePath, Sympathy.ToString());
    }

    public override string ToString()
    {
        return $"Имя: {Name}, Возраст: {Age}, Рост: {Height}, Кратко о себе: {ShortAboutSelf}, Описание персонажа: {AboutSelf}";
    }

    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>
    public void ResetSimpathy()
    {
        Sympathy = 0;
        File.WriteAllText(_filePath, (0).ToString());
        string json = File.ReadAllText(_filePath);
    }
    ///////////////////////////////////////////////////////////
}
