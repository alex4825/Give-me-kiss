using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteReceiver
{
    public static Sprite GetFromFile(string resourcesFilePath)
    {
        Sprite sprite = null;

        try
        {
            sprite = Resources.Load<Sprite>(resourcesFilePath);
        }
        catch
        {
            Debug.LogError($"����� {resourcesFilePath} �� ����������");
        }

        return sprite;
    }
}
