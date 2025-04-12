using System;
using System.IO;
using UnityEngine;

public abstract class Person
{
    protected float MaxProgressValue = 100;
    protected float MinProgressValue;

    public string OriginName { get; private set; }
    public string Name { get; private set; }
    public Sprite FaceSprite { get; private set; }
    public float Progress { get; protected set; }
    public Color BasicColor { get; private set; }
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

    public event Action<float> OnProgressChanged;

    public Person(PersonResourcesData personData)
    {
        OriginName = personData.OriginName;
        Name = personData.Name;
        FaceSprite = ResourcesFileLoader.LoadCharacterSpriteBy($"{OriginName}_face");
        BasicColor = personData.BasicColor;

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

        //SaveProgressToFile();
        Debug.Log($"Added {emotion.OriginName} with strength {emotion.Strength} for {OriginName}");

        OnProgressChanged?.Invoke(ProgressNormalized);
    }

}
