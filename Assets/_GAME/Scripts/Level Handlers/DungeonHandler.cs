using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string StartHint;
    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.Gameplay);
        if (StartHint != "")
        {
            UIManager.instance.GiveHint(UIManager.HintType.Objective, StartHint);
        }
    }

}
