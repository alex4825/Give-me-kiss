using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Person
{
    private string _filePath;
    private string ProgressFilePostfix = "_progress.json";
    private float _maxProgressValue = 100;
    private float _minProgressValue;

    public string OriginName { get; protected set; }
    public string Name { get; protected set; }
    public Sprite FaceSprite { get; protected set; }
    public Sprite AppearanceSprite { get; protected set; }
    public float Progress { get; private set; }
    public Color BasicColor { get; private set; }
    public float ProgressNormalized
    {
        get
        {
            if (Progress >= 0)
                return Progress / _maxProgressValue;
            else
                return Progress / -_minProgressValue;
        }
    }

    public event Action<float> OnProgressNormalizedChanged;

    public Person(PersonData personData)
    {
        OriginName = personData.OriginName;
        Name = personData.Name;
        FaceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_face");
        AppearanceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_full");
        BasicColor = personData.BasicColor;

        _filePath = Path.Combine(Application.persistentDataPath, OriginName + ProgressFilePostfix);

        Progress = LoadProgressFromFile();

        _minProgressValue = -_maxProgressValue;
    }

    public void AddProgressFrom(Emotion emotion)
    {
        Progress += emotion.Strength;

        if (Progress > _maxProgressValue)
        {
            Progress = _maxProgressValue;
        }
        else if (Progress < _minProgressValue)
        {
            Progress = _minProgressValue;
        }

        SaveProgressToFile();
        Debug.Log($"Added {emotion.OriginName} with strangth {emotion.Strength} for {OriginName}");

        OnProgressNormalizedChanged?.Invoke(ProgressNormalized);
    }

    private float LoadProgressFromFile()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            float progressValue = float.Parse(json);
            return progressValue;
        }

        return 0;
    }

    private void SaveProgressToFile()
    {
        File.WriteAllText(_filePath, Progress.ToString());
    }

    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>
    public void ResetProgress()
    {
        Progress = 0;
        File.Delete(_filePath);
        //string json = File.ReadAllText(_filePath);
    }

}
