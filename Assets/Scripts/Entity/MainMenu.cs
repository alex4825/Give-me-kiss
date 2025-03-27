using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ButtonHandler _playButton;
    [SerializeField] private ButtonHandler _settingsButton;
    [SerializeField] private ButtonHandler _aboutButton;
    [SerializeField] private ButtonHandler _exitButton;

    public static event Action OnPlayButtonClicked;

    private void Awake()
    {
        _playButton.OnClick += PlayButton_OnClick;
        _settingsButton.OnClick += SettingsButton_OnClick; ;
        _aboutButton.OnClick += AboutButton_OnClick; ;
        _exitButton.OnClick += ExitButton_OnClick; ;
    }


    private void PlayButton_OnClick()
    {
        OnPlayButtonClicked?.Invoke();
    }

    private void SettingsButton_OnClick()
    {

    }

    private void AboutButton_OnClick()
    {

    }

    private void ExitButton_OnClick()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
