using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelPreviewHandler : MonoBehaviour
{
    [SerializeField] private EventReference startRef;
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        AudioManager.instance.StopSong(FMOD.Studio.STOP_MODE.IMMEDIATE);
        InputManager.instance.controls.UI.Continue.performed += LoadLevel;
        switch (GameManager.instance.currentLevel + 1)
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
