using TMPro;
using UnityEngine;

public class CharismaObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _emotionText;

    public void SetValue(bool isPositive)
    {
        if (isPositive)
        {
            _emotionText.text = "������� ������� �������.";
            _emotionText.color = Color.green;
        }
        else
        {
            _emotionText.text = "������� ������� �������.";
            _emotionText.color = Color.red;
        }
    }
}
