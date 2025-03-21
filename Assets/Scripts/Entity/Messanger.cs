using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AiToolbox;
using System.Security.Cryptography;
using UnityEditor.VersionControl;

public class Messanger : MonoBehaviour
{
    [SerializeField] private Transform _messangesContainer;
    [SerializeField] private Transform _playerMessagePrefab;
    [SerializeField] private Transform _partnerMessagePrefab;
    [SerializeField] private TMP_InputField _inputField;

    private ChatGptParameters _gptParameters;

    private void Start()
    { 
        _gptParameters = new ChatGptParameters("")
        {
            model = ChatGptModel.Gpt35Turbo,
            temperature = 0.5f
        };
    }

    public void WritePlayerMessage()
    {
        string message = _inputField.text;

        if (!string.IsNullOrWhiteSpace(message))
        {
            Transform messageObject = Instantiate(_playerMessagePrefab, _messangesContainer);
            messageObject.GetComponent<Message>().InitiateMessage(GameManager.Instance.Player, message);
            _inputField.text = "";

            GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(message, Role.User));
            SendMessageToPartner();
        }
    }

    private void SendMessageToPartner()
    {
        Partner currentPartner = GameManager.Instance.CurrentPartner;

        ChatGpt.Request(
            currentPartner.Chat.History,
            _gptParameters,
            response =>
            {
                WritePartnerMessage(response);
            },
            (errorCode, errorMessage) =>
            {
                Debug.LogError($"Error receiving a response from {currentPartner.Name}.\n {errorCode}: {errorMessage}");
            }
        );
    }

    private void WritePartnerMessage(string response)
    {
        Partner currentPartner = GameManager.Instance.CurrentPartner;

        Transform messageObject = Instantiate(_partnerMessagePrefab, _messangesContainer);
        messageObject.GetComponent<Message>().InitiateMessage(currentPartner, response);

        currentPartner.Chat.Add(new AiToolbox.Message(response, Role.AI));
    }
}
