using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.U2D;

public class FileManager : Singleton<FileManager>
{
    [SerializeField] private string CharactersSpritesFolder = "Images/Characters/";
    [SerializeField] private string EmojiSpritesFolder = "Images/Emoji/";
    [SerializeField] private string JsonDataFolder = "JsonData/";
    [SerializeField] private string TextDataFolder = "TextData/";
    [SerializeField] private string PlayerDataJsonFileName = "PlayerData";
    [SerializeField] private string EmotionsDataJsonFileName = "EmotionsData";
    [SerializeField] private string PartnersDataJsonFileName = "PartnersData";
    [SerializeField] private string InitialInstructionsToAITxtFileName = "InitialInstructionsToAI";
    
    protected override void Awake()
    {
        base.Awake();
    }

    public string LoadInitialInstructionsToAI()
        => LoadTxtInString(TextDataFolder + InitialInstructionsToAITxtFileName);

    public Dictionary<string, EmotionData> JsonToEmotionDataDictionary()
    => JsonToDictionaryBy<EmotionData>(JsonDataFolder + EmotionsDataJsonFileName);

    public Dictionary<string, PartnerData> JsonToPartnerDataDictionary()
    => JsonToDictionaryBy<PartnerData>(JsonDataFolder + PartnersDataJsonFileName);

    public Sprite LoadCharacterSpriteBy(string originName)
    => LoadSpriteBy(CharactersSpritesFolder + originName);

    public Sprite LoadEmojiSpriteBy(string originName)
    => LoadSpriteBy(EmojiSpritesFolder + originName);

    public PlayerData JsonToPlayerData()
     => JsonToObjectBy<PlayerData>(JsonDataFolder + PlayerDataJsonFileName);

    private Dictionary<string, T> JsonToDictionaryBy<T>(string resourcesJsonFile)
    {
        Dictionary<string, T> keyValuePairs = new Dictionary<string, T>();

        TextAsset jsonFile = Resources.Load<TextAsset>(resourcesJsonFile);

        if (jsonFile != null)
        {
            keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/{resourcesJsonFile}.json doesn't exist!");
        }

        return keyValuePairs;
    }

    private T JsonToObjectBy<T>(string resourcesJsonFile)
    {
        T result = default;

        TextAsset jsonFile = Resources.Load<TextAsset>(resourcesJsonFile);

        if (jsonFile != null)
        {
            result = JsonConvert.DeserializeObject<T>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/{resourcesJsonFile}.json doesn't exist!");
        }

        return result;
    }

    private Sprite LoadSpriteBy(string resourcesSpriteFile)
    {
        Sprite sprite = null;

        try
        {
            sprite = Resources.Load<Sprite>(resourcesSpriteFile);
        }
        catch
        {
            Debug.LogError($"File Resources/{resourcesSpriteFile} doesn't exist.");
        }

        return sprite;
    }

    private string LoadTxtInString(string resourcesTxtFile)
    {
        TextAsset txtFile = new TextAsset();

        try
        {
            txtFile = Resources.Load<TextAsset>(resourcesTxtFile);
        }
        catch
        {
            Debug.LogError($"File Resources/{resourcesTxtFile} doesn't exist.");
        }

        return txtFile.text;
    }

    //public List<AiToolbox.Message> LoadMessageHistoryWith(Person person)
    //{
    //    string filePath = TextDataFolder + MessageHistoryFilePrefix + person.OriginName;

    //    TextAsset messageHistoryFile = Resources.Load<TextAsset>(filePath);

    //    if (messageHistoryFile == null)
    //    {
    //        Debug.LogError($"File Resources/{filePath} doesn't exist.");
    //        return null;
    //    }

    //}
}
