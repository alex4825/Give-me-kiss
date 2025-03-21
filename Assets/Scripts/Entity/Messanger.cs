using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Messanger : MonoBehaviour
{
    [SerializeField] private Transform _messangesContainer;
    [SerializeField] private Transform _playerMessagePrefab;
    [SerializeField] private Transform _partnerMessagePrefab;
    [SerializeField] private TMP_InputField _inputField;

    public void SendPlayerMessage()
    {
        string message = _inputField.text;

        if (!string.IsNullOrWhiteSpace(message))
        {
            Transform messageObject = Instantiate(_playerMessagePrefab, _messangesContainer);
            messageObject.GetComponent<Message>().InitiateMessage(GameManager.Instance.Player, message);
            _inputField.text = "";
        }
    }
}
