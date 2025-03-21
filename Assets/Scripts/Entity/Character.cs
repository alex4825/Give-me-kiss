using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Character
{
    private string _originName;
    private string _name;
    private int _age;
    private int _height;
    private string _aboutSelf;
    private Sprite _faceSprite;
    private Sprite _appearanceSprite;
    private const string ResourcesImagesFolder = "Images/Characters/";

    public Character(string originName, CharacterData characterData)
    {
        _originName = originName;
        _name = characterData.Name;
        _age = characterData.Age;
        _height = characterData.Height;
        _aboutSelf = characterData.AboutSelf;

        _faceSprite = SpriteReceiver.GetFromFile($"{ResourcesImagesFolder}{originName}_face");
        _appearanceSprite = SpriteReceiver.GetFromFile($"{ResourcesImagesFolder}{originName}_full");
    }

    public string OriginName => _originName;
    public string Name => _name;
    public int Age => _age;
    public int Height => _height;
    public string AboutSelf => _aboutSelf;
    public Sprite FaceSprite => _faceSprite;
    public Sprite AppearanceSprite => _appearanceSprite;

    public override string ToString()
    {
        return $"Имя: {_name}, возраст: {_age}, рост: {_height}.";
    }
}
