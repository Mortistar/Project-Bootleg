using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelPreviewHandler : MonoBehaviour
{
    [SerializeField] private EventReference startRef;
    [SerializeField] private Image levelText;
    [SerializeField] private Sprite sprZero;
    [SerializeField] private Sprite sprOne;
    [SerializeField] private Sprite sprTwo;
    [SerializeField] private Sprite sprThree;
    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        AudioManager.instance.StopSong(FMOD.Studio.STOP_MODE.IMMEDIATE);
        InputManager.instance.controls.UI.Continue.performed += LoadLevel;
        switch (GameManager.instance.currentLevel + 1)
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
    }
    private void LoadLevel(InputAction.CallbackContext ctx)
    {
        RuntimeManager.PlayOneShot(startRef);
        GameManager.instance.AdvanceLevel();
    }
    void OnDisable()
    {
        InputManager.instance.controls.UI.Continue.performed -= LoadLevel;
    }
}
