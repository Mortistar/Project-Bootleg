using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int secretCount;
    [SerializeField] private string StartHint;
    [SerializeField] private AudioManager.Song songToPlay;
    void Start()
    {
        AudioManager.instance.PlaySong(songToPlay, FMOD.Studio.STOP_MODE.IMMEDIATE);
        InputManager.instance.SetControls(InputManager.ControlType.Gameplay);
        if (StartHint != "")
        {
            UIManager.instance.GiveHint(UIManager.HintType.Objective, StartHint);
        }
        if (secretCount > 0)
        {
            GameManager.instance.InitialiseDungeon(secretCount);
        }else
        {
            GameManager.instance.InitialiseDungeon();
        }
    }
    void OnDisable()
    {
        UIManager.instance.ClearHint();
        AudioManager.instance.StopSong(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
