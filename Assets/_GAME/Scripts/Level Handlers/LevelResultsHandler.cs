using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMODUnity;

public class LevelResultsHandler : MonoBehaviour
{
    [SerializeField] private EventReference startRef;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    [SerializeField] private TMPro.TextMeshProUGUI enemyText;
    [SerializeField] private TMPro.TextMeshProUGUI timeText;
    [SerializeField] private TMPro.TextMeshProUGUI secretText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        AudioManager.instance.StopSong(FMOD.Studio.STOP_MODE.IMMEDIATE);
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
                levelText.text = "E1M0 : TUTORIAL";
                break;
            case LevelManager.LevelIndex.DungeonOne:
                levelText.text = "E1M1 : ENTRANCE HALL";
                break;
            case LevelManager.LevelIndex.DungeonTwo:
                levelText.text = "E1M2 : PRISON";
                break;
            case LevelManager.LevelIndex.DungeonThree:
                levelText.text = "E1M3 : RITUAL CHAMBER";
                break;
        }
        enemyText.text = GameManager.instance.dungeonData.enemiesKilled.ToString();
        timeText.text = GameManager.instance.dungeonData.GetTimeString();
        secretText.text = string.Format("{0}/{1}", GameManager.instance.dungeonData.secretsFound, GameManager.instance.dungeonData.secretsTotal);
        scoreText.text = GameManager.instance.dungeonData.score.ToString();
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
