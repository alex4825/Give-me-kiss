using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{    
    [SerializeField] private string _resourcesPlayerFileName;

    public Player Player { get; private set; }
    public Character CurrentCharacter { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Player = new Player(_resourcesPlayerFileName);
    }

    public void SetCurrent(Character character)
    {

    }
}
