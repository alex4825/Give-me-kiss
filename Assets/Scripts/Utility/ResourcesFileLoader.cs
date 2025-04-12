using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public static class ResourcesFileLoader
{
    private static string CharactersSpritesFolder = "Images/Characters/";
    private static string EmojiSpritesFolder = "Images/Emoji/";
    private static string JsonDataFolder = "JsonData/";
    private static string TextDataFolder = "TextData/";
    private static string PlayerDataJsonFileName = "PlayerData";
    private static string EmotionsDataJsonFileName = "EmotionsData";
    private static string PartnersDataJsonFileName = "PartnersData";
    private static string InitialInstructionsToAITxtFileName = "InitialInstructionsToAI";
    private static string ApiKeyGPTTxtFileName = "ApiKeyGPT";

    public static string LoadApiKeyGPT()
    => LoadTxtInString(TextDataFolder + ApiKeyGPTTxtFileName);

    public static string LoadInitialInstructionsToAI()
        => LoadTxtInString(TextDataFolder + InitialInstructionsToAITxtFileName);

    public static List<EmotionResourcesData> JsonToEmotionDataList()
    => JsonToListBy<EmotionResourcesData>(JsonDataFolder + EmotionsDataJsonFileName);

    public static List<PartnerResourcesData> JsonToPartnerDataList()
    => JsonToListBy<PartnerResourcesData>(JsonDataFolder + PartnersDataJsonFileName);

    public static Sprite LoadCharacterSpriteBy(string originName)
    => LoadSpriteBy(CharactersSpritesFolder + originName);

    public static Sprite LoadEmojiSpriteBy(string originName)
    => LoadSpriteBy(EmojiSpritesFolder + originName);

    public static PlayerResourcesData JsonToPlayerData()
     => JsonToObjectBy<PlayerResourcesData>(JsonDataFolder + PlayerDataJsonFileName);

    private static Dictionary<string, T> JsonToDictionaryBy<T>(string resourcesJsonFile)
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

    private static List<T> JsonToListBy<T>(string resourcesJsonFile)
    {
        List<T> list = new List<T>();

        TextAsset jsonFile = Resources.Load<TextAsset>(resourcesJsonFile);

        if (jsonFile != null)
        {
            list = JsonConvert.DeserializeObject<List<T>>(jsonFile.text);
        }
        else
        {
            Debug.LogError($"File Resources/{resourcesJsonFile}.json doesn't exist!");
        }

        return list;
    }

    private static T JsonToObjectBy<T>(string resourcesJsonFile)
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

    private static Sprite LoadSpriteBy(string resourcesSpriteFile)
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

    private static string LoadTxtInString(string resourcesTxtFile)
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
