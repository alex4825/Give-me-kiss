using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    public Player(PlayerData playerData) : base(playerData as PersonData)
    {
        
    }    
}
