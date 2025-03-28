using System.Collections.Generic;
using UnityEngine;

public static class DebugInformer
{
    public static void ShowStringFrom<T>(List<T> list)
    {
        string str = string.Empty;

        foreach (T item in list)
        {
            str += item.ToString();
        }

        Debug.Log(str);
    }
}
