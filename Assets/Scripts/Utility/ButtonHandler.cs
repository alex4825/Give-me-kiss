using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _buttonImage;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _enterColor;
    [SerializeField] private float _enterScale = 1.1f;
    [SerializeField] private float _duration = 0.2f;

    private float _initialScale;

    public event Action OnClick;

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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        OnClick?.Invoke();
        Debug.Log($"Button {gameObject.name} clicked.");
    }

}
