using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    public Player Player { get; private set; }
    public Partner CurrentPartner { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        Player = new Player();
        CurrentPartner = null;
    }

    public void SetCurrent(Partner partner)
    {
        CurrentPartner = partner;
    }
}
