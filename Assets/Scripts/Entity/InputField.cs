using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputField : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private ButtonHandler _sendButton;

    private TMP_InputField _inputFieldTMP;
    private GameObject _placeholder;
    private Outline _outline;
    private float _scale = 2;

    public event Action<string> OnMessageSent;

    void Awake()
    {
        _inputFieldTMP = GetComponent<TMP_InputField>();
        _placeholder = _inputFieldTMP.placeholder.gameObject;
        _outline = GetComponent<Outline>();

        KeyManager.OnEnterKeyPressed += SendPlayerMessage;
        _sendButton.OnClick += SendPlayerMessage;
    }

    private void SendPlayerMessage()
    {
        if (!string.IsNullOrWhiteSpace(_inputFieldTMP.text))
        {
            OnMessageSent?.Invoke(_inputFieldTMP.text);
            _inputFieldTMP.text = string.Empty;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        _placeholder.SetActive(false);
        _outline.effectDistance *= _scale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (string.IsNullOrEmpty(_inputFieldTMP.text))
        {
            _placeholder.SetActive(true);
        }

        _outline.effectDistance /= _scale;
    }

}
