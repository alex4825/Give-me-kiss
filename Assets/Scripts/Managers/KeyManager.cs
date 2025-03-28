using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : Singleton<KeyManager>
{
    public static event Action OnEnterKeyPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterKeyPressed?.Invoke();
        }
    }
}
