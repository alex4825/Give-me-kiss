using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SympathyBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;

    public void SetFillAmount(float sympathyValue)
    {
        _barImage.fillAmount = sympathyValue;
    }
}
