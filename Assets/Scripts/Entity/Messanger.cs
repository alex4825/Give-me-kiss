using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AiToolbox;
using System.Security.Cryptography;
using UnityEditor.VersionControl;
using System.IO;
using TreeEditor;
using Newtonsoft.Json;

public class Messanger : MonoBehaviour
{
    [SerializeField] private Transform _messangesContainer;
    [SerializeField] private Transform _buttonBack;
    [SerializeField] private Transform _messagePrefab;
    [SerializeField] private TMP_InputField _inputField;

    private ChatGptParameters _gptParameters;
    private List<Transform> _visibleMessages;
    private string _initialMessage;
    private bool _isInitiated;

    public static event Action OnButtonBackPressed;

    private void Awake()
    {
        _gptParameters = new ChatGptParameters("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")
        {
            model = ChatGptModel.Gpt35Turbo,
            temperature = 0.5f
        };

        _visibleMessages = new List<Transform>();
    }

    private void OnEnable()
    {
        _isInitiated = GameManager.Instance.CurrentPartner.Chat.IsInitialized;

        if (_isInitiated)
        {
            FillContainerFromChatHistory();
        }
        else
        {
            _initialMessage = GetInitiateMessage();
        }
    }

    private string GetInitiateMessage()
    {
        string initiateMessage = FileManager.Instance.LoadInitialInstructionsToAI();

        string aboutAmotions = $"emotion ����� ��������� ����� ��������: {EmotionManager.Instance.GetEmotionsInString()}.";
        string aboutCurrentPartner = $"���� ��������: {GameManager.Instance.CurrentPartner.ToString()}.";
        string closingMessage = $"������ ���������� ���� ��������� � �������. _INSTRUCT_END.";

        initiateMessage += aboutAmotions + aboutCurrentPartner + closingMessage;
        Debug.Log($"Initial message for {GameManager.Instance.CurrentPartner.OriginName} is:\n {initiateMessage}");

        return initiateMessage;
    }

    private void FillContainerFromChatHistory()
    {
        foreach (var message in GameManager.Instance.CurrentPartner.Chat.History)
        {
            if (message.text.Contains("_INSTRUCT"))
                continue;

            if (message.role == Role.User)
                WriteMessageToContainerFrom(GameManager.Instance.Player, message.text);
            else if (message.role == Role.AI)
                WriteMessageToContainerFrom(GameManager.Instance.CurrentPartner, message.text);
        }
    }

    public void SendPlayerMessageFromInput()
    {
        string message = _inputField.text;

        if (!string.IsNullOrWhiteSpace(message))
        {
            WriteMessageToContainerFrom(GameManager.Instance.Player, message);
            _inputField.text = string.Empty;

            if (_isInitiated == false)
            {
                GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(_initialMessage, Role.User));
                _isInitiated = true;
            }

            GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(message, Role.User));

            SendMessageToPartner();
        }
    }

    private void WriteMessageToContainerFrom(Person person, string message)
    {
        Transform messageObject = Instantiate(_messagePrefab, _messangesContainer);

        if (person is Partner)
        {
            if (message.StartsWith("{") && message.EndsWith("}"))
            {
                AiMessageData messageData = JsonConvert.DeserializeObject<AiMessageData>(message);
                message = messageData.Message;
            }
            else
            {
                Debug.LogWarning("�������� �� JSON-���������: " + message);
            }
        }

        messageObject.GetComponent<Message>().InitiateMessageFor(person, message);
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
                GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(response, Role.AI));
            },
            (errorCode, errorMessage) =>
            {
                Debug.LogError($"Error receiving a response from {currentPartner.Name}.\n {errorCode}: {errorMessage}");
            }
        );
    }

    public void GoToPartnerChoosing()
    {
        OnButtonBackPressed?.Invoke();
    }

    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.CurrentPartner.Chat.Clear();

            ClearChat();

            _isInitiated = false;
        }
    }
    /////////////////////////////////////////////////////////

    private void OnDisable()
    {
        ClearChat();
    }

    private void ClearChat()
    {
        foreach (Transform message in _visibleMessages)
        {
            Destroy(message.gameObject);
        }

        _visibleMessages.Clear();
    }
}
