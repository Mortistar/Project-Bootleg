using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour
{

    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnQuit;
    [SerializeField] private EventReference clickRef;

    void Start()
    {
        btnPlay.onClick.AddListener(OnPlayClicked);
        btnSettings.onClick.AddListener(OnSettingsClicked);
        btnQuit.onClick.AddListener(OnQuitClicked);
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        AudioManager.instance.PlaySong(AudioManager.Song.MainMenu, FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
    private void OnPlayClicked()
    {
        RuntimeManager.PlayOneShot(clickRef);
        OnDisable();
        LevelManager.instance.LevelLoad(LevelManager.LevelIndex.LevelPreview);
    }
    private void OnSettingsClicked()
    {
        OnDisable();
        RuntimeManager.PlayOneShot(clickRef);
    }
    private void OnQuitClicked()
    {
        OnDisable();
        RuntimeManager.PlayOneShot(clickRef);
        GameManager.instance.Quit(1f);
    }
    
    void OnDisable()
    {
        btnPlay.onClick.RemoveListener(OnPlayClicked);
        btnSettings.onClick.RemoveListener(OnSettingsClicked);
        btnQuit.onClick.RemoveListener(OnQuitClicked);
    }
}
