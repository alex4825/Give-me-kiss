using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Person
{
    private string _fileProgressPath;
    private string ProgressFilePostfix = "_progress.json";
    protected float MaxProgressValue = 20;
    protected float MinProgressValue;

    public string OriginName { get; protected set; }
    public string Name { get; protected set; }
    public Sprite FaceSprite { get; protected set; }
    public Sprite AppearanceSprite { get; protected set; }
    public float Progress { get; protected set; }
    public Color BasicColor { get; protected set; }
    public float ProgressNormalized
    {
        get
        {
            if (Progress >= 0)
                return Progress / MaxProgressValue;
            else
                return Progress / -MinProgressValue;
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

        _fileProgressPath = Path.Combine(Application.persistentDataPath, OriginName + ProgressFilePostfix);

        Progress = LoadProgressFromFile();

        MinProgressValue = -MaxProgressValue * 0.8f;
    }

    public virtual void AddProgressFrom(Emotion emotion)
    {
        Progress += emotion.Strength;

        if (Progress > MaxProgressValue)
        {
            Progress = MaxProgressValue;
        }
        else if (Progress < MinProgressValue)
        {
            Progress = MinProgressValue;
        }

        SaveProgressToFile();
        Debug.Log($"Added {emotion.OriginName} with strangth {emotion.Strength} for {OriginName}");

        OnProgressNormalizedChanged?.Invoke(ProgressNormalized);
    }

    private float LoadProgressFromFile()
    {
        if (File.Exists(_fileProgressPath))
        {
            string json = File.ReadAllText(_fileProgressPath);
            float progressValue = float.Parse(json);
            return progressValue;
        }

        return 0;
    }

    private void SaveProgressToFile()
    {
        File.WriteAllText(_fileProgressPath, Progress.ToString());
    }

    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>
    public virtual void ResetFiles()
    {
        Progress = 0;
        File.Delete(_fileProgressPath);
        //string json = File.ReadAllText(_filePath);
    }

}
