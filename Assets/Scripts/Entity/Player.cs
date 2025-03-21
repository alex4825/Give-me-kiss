using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string _originName;
    private string _name;
    private Sprite _faceSprite;
    private Sprite _appearanceSprite;

    public string OriginName => _originName;
    public string Name => _name; 
    public Sprite FaceSprite => _faceSprite;
    public Sprite AppearanceSprite => _appearanceSprite;

    public Player()
    {
        PlayerData playerData = FileManager.Instance.JsonToPlayerData();

        _originName = playerData.OriginName;
        _name = playerData.Name;
        _faceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{_originName}_face");
        _appearanceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{_originName}_full");
    }
}
