using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileManager : SingletonPersistent<FileManager>
{
    [SerializeField] private string CharactersSpritesFolder = "Images/Characters/";
    [SerializeField] private string EmojiSpritesFolder = "Images/Emoji/";
    [SerializeField] private string JsonDataFolder = "JsonData/";
    [SerializeField] private string PlayerDataFileName = "PlayerData";
    [SerializeField] private string EmotionsDataFileName = "EmotionsData";
    [SerializeField] private string PartnersDataFileName = "PartnersData";

    protected override void Awake()
    {
        base.Awake();
    }

    public Dictionary<string, EmotionData> JsonToEmotionDataDictionary()
    => JsonToDictionaryBy<EmotionData>(JsonDataFolder + EmotionsDataFileName);

    public Dictionary<string, PartnerData> JsonToPartnerDataDictionary()
    => JsonToDictionaryBy<PartnerData>(JsonDataFolder + PartnersDataFileName);

    public Sprite LoadCharacterSpriteBy(string originName)
    => LoadSpriteBy(CharactersSpritesFolder + originName);

    public Sprite LoadEmojiSpriteBy(string originName)
    => LoadSpriteBy(EmojiSpritesFolder + originName);

    public PlayerData JsonToPlayerData()
     => JsonToObjectBy<PlayerData>(JsonDataFolder + PlayerDataFileName);

    private Dictionary<string, T> JsonToDictionaryBy<T>(string resourcesFile)
    {
        Dictionary<string, T> keyValuePairs = new Dictionary<string, T>();

        TextAsset jsonFile = Resources.Load<TextAsset>(resourcesFile);

        if (jsonFile != null)
        {
            keyValuePairs = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/{resourcesFile}.json doesn't exist!");
        }

        return keyValuePairs;
    }

    private T JsonToObjectBy<T>(string resourcesFile)
    {
        T result = default;

        TextAsset jsonFile = Resources.Load<TextAsset>(resourcesFile);

        if (jsonFile != null)
        {
            result = JsonConvert.DeserializeObject<T>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/{resourcesFile}.json doesn't exist!");
        }

        return result;
    }

    private Sprite LoadSpriteBy(string resourcesFile)
    {
        Sprite sprite = null;

        try
        {
            sprite = Resources.Load<Sprite>(resourcesFile);
        }
        catch
        {
            Debug.LogError($"File Resources/{resourcesFile} doesn't exist.");
        }

        return sprite;
    }
}
