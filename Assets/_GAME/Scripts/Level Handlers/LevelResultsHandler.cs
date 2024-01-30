using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;
using UnityEngine.UI;

public class LevelResultsHandler : MonoBehaviour
{
    [SerializeField] private EventReference startRef;
    [SerializeField] private Image levelText;
    [SerializeField] private Sprite sprZero;
    [SerializeField] private Sprite sprOne;
    [SerializeField] private Sprite sprTwo;
    [SerializeField] private Sprite sprThree;
    [SerializeField] private TMPro.TextMeshProUGUI enemyText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI secretText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        AudioManager.instance.PlaySong(AudioManager.Song.Results, FMOD.Studio.STOP_MODE.IMMEDIATE);
        InputManager.instance.controls.UI.Continue.performed += LoadLevel;
        if (GameManager.instance.dungeonData != null)
        {
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
        switch (GameManager.instance.currentLevel)
        {
            case LevelManager.LevelIndex.Tutorial:
                levelText.sprite = sprZero;
                break;
            case LevelManager.LevelIndex.DungeonOne:
                levelText.sprite = sprOne;
                break;
            case LevelManager.LevelIndex.DungeonTwo:
                levelText.sprite = sprTwo;
                break;
            case LevelManager.LevelIndex.DungeonThree:
                levelText.sprite = sprThree;
                break;
        }
        enemyText.text = GameManager.instance.dungeonData.enemiesKilled.ToString();
        timeText.text = GameManager.instance.dungeonData.GetTimeString();
        secretText.text = string.Format("{0}/{1}", GameManager.instance.dungeonData.secretsFound, GameManager.instance.dungeonData.secretsTotal);
        if (GameManager.instance.dungeonData.score == 0)
        {
            scoreText.text = "PACIFIST";
        }else
        {
            scoreText.text = GameManager.instance.dungeonData.score.ToString();
        }
        
    }
    private void LoadLevel(InputAction.CallbackContext ctx)
    {
        RuntimeManager.PlayOneShot(startRef);
        if (GameManager.instance.currentLevel == LevelManager.LevelIndex.DungeonThree)
        {
            LevelManager.instance.LevelLoad(LevelManager.LevelIndex.Credits);
        }else
        {
            LevelManager.instance.LevelLoad(LevelManager.LevelIndex.LevelPreview);
        }
    }
    void OnDisable()
    {
        InputManager.instance.controls.UI.Continue.performed -= LoadLevel;
    }
}
