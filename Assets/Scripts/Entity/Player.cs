using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    public Player()
    {
        PlayerData playerData = FileManager.Instance.JsonToPlayerData();

        OriginName = playerData.OriginName;
        Name = playerData.Name;
        FaceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_face");
        AppearanceSprite = FileManager.Instance.LoadCharacterSpriteBy($"{OriginName}_full");
    }    
}
