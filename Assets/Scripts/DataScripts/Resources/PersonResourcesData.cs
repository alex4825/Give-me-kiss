using Newtonsoft.Json;
using UnityEngine;

public abstract class PersonResourcesData : MonoBehaviour
{
    [JsonProperty("basicColor")]
    protected string HexColor
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

    public Color BasicColor { get; private set; }
}
