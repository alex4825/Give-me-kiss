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
    [SerializeField] private ButtonHandler _backButton;
    [SerializeField] private Transform _messagePrefab;
    [SerializeField] private TMP_InputField _inputField;

    private ChatGptParameters _gptParameters;
    private List<Transform> _visibleMessages;
    private string _initialMessage;
    private bool _isInitiated;

    public static event Action OnBackButtonClicked;

    private void Awake()
    {
        _gptParameters = new ChatGptParameters("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")
        {
            model = ChatGptModel.Gpt35Turbo,
            temperature = 0.5f
        };

        _visibleMessages = new List<Transform>();

        _backButton.OnClick += BackButton_OnClick;
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
    private void BackButton_OnClick()
    {
        OnBackButtonClicked?.Invoke();
    }

    private string GetInitiateMessage()
    {
        string initiateMessage = FileManager.Instance.LoadInitialInstructionsToAI();

        string aboutAmotions = $"emotion может принимать такие значени€: {EmotionManager.Instance.GetEmotionsInString()}.";
        string aboutCurrentPartner = $"“вой персонаж: {GameManager.Instance.CurrentPartner.ToString()}.";
        string closingMessage = $"“еперь начинаетс€ тво€ переписка с игроком. _INSTRUCT_END.";

        initiateMessage += aboutAmotions + aboutCurrentPartner + closingMessage;
        Debug.Log($"Initial message for {GameManager.Instance.CurrentPartner.OriginName} is:\n {initiateMessage}");

        return initiateMessage;
    }

    private void FillContainerFromChatHistory()
    {
        List<AiToolbox.Message> messages = GameManager.Instance.CurrentPartner.Chat.History;

        foreach (AiToolbox.Message message in messages)
        {
            if (message.text.Contains("_INSTRUCT"))
                continue;

            if (message.role == Role.User)
                WriteMessageToContainerFrom(GameManager.Instance.Player, message);
            else if (message.role == Role.AI)
                WriteMessageToContainerFrom(GameManager.Instance.CurrentPartner, message);
        }
    }

    public void SendPlayerMessageFromInput()
    {
        string messageText = _inputField.text;

        if (!string.IsNullOrWhiteSpace(messageText))
        {
            WriteMessageToContainerFrom(GameManager.Instance.Player, new AiToolbox.Message(messageText, Role.User));
            _inputField.text = string.Empty;

            if (_isInitiated == false)
            {
                GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(messageText, Role.User));
                _isInitiated = true;
            }

            GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(messageText, Role.User));

            SendMessageToPartner();
        }
    }

    private void WriteMessageToContainerFrom(Person person, AiToolbox.Message message)
    {
        Transform messageObject = Instantiate(_messagePrefab, _messangesContainer);

        if (person is Partner)
        {
            if (message.text.StartsWith("{") && message.text.EndsWith("}"))
            {
                AiMessageData messageData = JsonConvert.DeserializeObject<AiMessageData>(message.text);
                message.text = messageData.Message;
            }
            else
            {
                Debug.LogWarning("ѕолучено не JSON-сообщение: " + message);
            }
        }

        messageObject.GetComponent<MessageObject>().InitiateMessageFor(person, message);
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
                WriteMessageToContainerFrom(currentPartner, new AiToolbox.Message(response, Role.AI));
                GameManager.Instance.CurrentPartner.Chat.Add(new AiToolbox.Message(response, Role.AI));
            },
            (errorCode, errorMessage) =>
            {
                Debug.LogError($"Error receiving a response from {currentPartner.Name}.\n {errorCode}: {errorMessage}");
            }
        );
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
