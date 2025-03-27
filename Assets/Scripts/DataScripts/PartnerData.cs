using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PartnerData : MonoBehaviour
{
    [JsonProperty("basicColor")]
    private string HexColor
    {
        get => ColorUtility.ToHtmlStringRGBA(BasicColor);
        set
        {
            if (ColorUtility.TryParseHtmlString("#" + value, out var color))
            {
                BasicColor = color;
            }
            else
            {
                BasicColor = Color.white;
            }
        }
    }
    [JsonProperty("originName")]
    public string OriginName { get; private set; }

    [JsonProperty("name")]
    public string Name { get; private set; }

    [JsonProperty("age")]
    public int Age { get; private set; }

    [JsonProperty("height")]
    public int Height { get; private set; }

    [JsonProperty("shortAboutSelf")]
    public string ShortAboutSelf { get; private set; }

    [JsonProperty("aboutSelf")]
    public string AboutSelf { get; private set; }

    public Color BasicColor { get; private set; }

}
