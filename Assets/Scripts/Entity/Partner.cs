using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Partner : Person
{
    public Partner(string originName, PartnerData characterData)
    {
        OriginName = originName;
        Name = characterData.Name;
        Age = characterData.Age;
        Height = characterData.Height;
        AboutSelf = characterData.AboutSelf;
        FaceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_face");
        AppearanceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_full");
        Chat = new Chat(this);
    }

    public int Age { get; private set; }
    public int Height { get; private set; }
    public string AboutSelf { get; private set; }
    public Chat Chat { get; private set; }

}
