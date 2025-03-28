using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SympathyBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    [SerializeField] private Color _positiveColor;
    [SerializeField] private Color _negativeColor;

    public void SetFillAmount(float sympathyValueNormalized)
    {
        if (sympathyValueNormalized >= 0)
        {
            _barImage.fillAmount = sympathyValueNormalized;
            _barImage.color = _positiveColor;
        }
        else
        {
            _barImage.fillAmount = -sympathyValueNormalized;
            _barImage.color = _negativeColor;
        }
    }
}
