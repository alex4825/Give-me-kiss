using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string _originName;
    private string _name;
    private Sprite _faceSprite;
    private Sprite _appearanceSprite;
    private const string ResourcesImagesFolder = "Images/Characters/";

    public string OriginName => _originName;
    public string Name => _name; 
    public Sprite FaceSprite => _faceSprite;
    public Sprite AppearanceSprite => _appearanceSprite;

    public Player(string playerDataFile)
    {
        PlayerData playerData = JsonConverter.ToObject<PlayerData>(playerDataFile);

        _originName = playerData.OriginName;
        _name = playerData.Name;
        _faceSprite = SpriteReceiver.GetFromFile($"{ResourcesImagesFolder}{_originName}_face");
        _appearanceSprite = SpriteReceiver.GetFromFile($"{ResourcesImagesFolder}{_originName}_full");
    }
}
