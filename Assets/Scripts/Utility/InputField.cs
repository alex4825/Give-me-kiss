using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private TMP_InputField _inputField;
    private GameObject _placeholder;
    private Outline _outline;
    private float _scale = 2;

    void Awake()
    {
        _inputField = GetComponent<TMP_InputField>();
        _placeholder = _inputField.placeholder.gameObject;
        _outline = GetComponent<Outline>(); 
    }

    public void OnSelect(BaseEventData eventData)
    {
        _placeholder.SetActive(false);
        _outline.effectDistance *= _scale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (string.IsNullOrEmpty(_inputField.text))
        {
            _placeholder.SetActive(true);
        }

        _outline.effectDistance /= _scale;
    }

}
