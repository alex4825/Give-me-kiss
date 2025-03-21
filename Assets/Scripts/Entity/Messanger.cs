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
    [SerializeField] private Transform _messagePrefab;
    [SerializeField] private TMP_InputField _inputField;

    private ChatGptParameters _gptParameters;
    private List<Transform> _visibleMessages;

    private void Awake()
    {
        _gptParameters = new ChatGptParameters("")
        {
            model = ChatGptModel.Gpt35Turbo,
            temperature = 0.5f
        };

        _visibleMessages = new List<Transform>();
    }

    private void OnEnable()
    {
        FillFromChatHistory();
    }


    private void FillFromChatHistory()
    {
        Partner partner = GameManager.Instance.CurrentPartner;
        Player player = GameManager.Instance.Player;

        foreach (var message in partner.Chat.History)
        {
            if (message.role == Role.User)
            {
                WriteMessageToContainerFrom(player, message.text);
            }
            else if (message.role == Role.AI)
            {
                WriteMessageToContainerFrom(partner, message.text);
            }
        }
    }

    public void WritePlayerMessageFromInput()
    {
        string message = _inputField.text;

        if (!string.IsNullOrWhiteSpace(message))
        {
            WriteMessageToContainerFrom(GameManager.Instance.Player, message);
            _inputField.text = "";

            GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(message, Role.User));
            SendMessageToPartner();
        }
    }

    private void WriteMessageToContainerFrom(Person person, string message)
    {
        Transform messageObject = Instantiate(_messagePrefab, _messangesContainer);
        messageObject.GetComponent<Message>().InitiateMessage(person, message);
        _visibleMessages.Add(messageObject);
    }

    private void SendMessageToPartner()
    {
        Partner currentPartner = GameManager.Instance.CurrentPartner;

        ChatGpt.Request(
            currentPartner.Chat.History,
            _gptParameters,
            response =>
            {
                WriteMessageToContainerFrom(currentPartner, response);
            },
            (errorCode, errorMessage) =>
            {
                Debug.LogError($"Error receiving a response from {currentPartner.Name}.\n {errorCode}: {errorMessage}");
            }
        );
    }

    private void OnDisable()
    {
        ClearChat();
    }

    private void ClearChat()
    {
        foreach (Transform message in _visibleMessages)
        {
            Destroy(message);
        }

        _visibleMessages.Clear();
    }
}
