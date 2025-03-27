using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _buttonImage;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _enterColor;
    [SerializeField] private float _enterScale;
    [SerializeField] private float _duration = 0.2f;

    private float _initialScale;

    private void Start()
    {
        _buttonImage.color = _normalColor;
        _initialScale = transform.localScale.x;
    }

    private void OnDisable()
    {
        _buttonImage.color = _normalColor;
        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _buttonImage.DOColor(_enterColor, _duration);
        transform.DOScale(_enterScale, _duration);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _buttonImage.DOColor(_normalColor, _duration);
        transform.DOScale(_initialScale, _duration);
    }

}
