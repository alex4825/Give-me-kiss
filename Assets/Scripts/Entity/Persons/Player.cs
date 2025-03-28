using System;
using System.IO;
using UnityEngine;

public class Player : Person
{
    private string _fileCharizmaLevelPath;
    private string CharismaLevelFilePostfix = "_charismaLevel.json";

    public Player(PlayerData playerData) : base(playerData as PersonData)
    {
        _fileCharizmaLevelPath = Path.Combine(Application.persistentDataPath, OriginName + CharismaLevelFilePostfix);

        CharismaLevel = LoadCharismaLevelFromFile();
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

            SaveCharismaLevelToFile();
            Progress = 0;

            OnCharismaLevelUp?.Invoke(CharismaLevel);

            Debug.Log($"Уровень харизмы повышен до {CharismaLevel}");
        }
        else if (Progress <= MinProgressValue)
        {
            CharismaLevel--;

            if (CharismaLevel == 0)
                CharismaLevel = -1;

            SaveCharismaLevelToFile();
            Progress = 0;

            OnCharismaLevelDown?.Invoke(CharismaLevel);

            Debug.Log($"Уровень харизмы понижен до {CharismaLevel}");
        }
    }


    private int LoadCharismaLevelFromFile()
    {
        if (File.Exists(_fileCharizmaLevelPath))
        {
            string json = File.ReadAllText(_fileCharizmaLevelPath);
            int charismaLevelValue = int.Parse(json);
            return charismaLevelValue;
        }
        else
        {
            CharismaLevel = 1;
            SaveCharismaLevelToFile();
        }

        return 0;
    }

    private void SaveCharismaLevelToFile()
    {
        File.WriteAllText(_fileCharizmaLevelPath, CharismaLevel.ToString());
    }


    /// <summary>
    /// //////////////////////////////////////////////////
    /// </summary>
    public override void ResetFiles()
    {
        base.ResetFiles();

        CharismaLevel = 1;
        File.Delete(_fileCharizmaLevelPath);
    }
}
