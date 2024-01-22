using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.Gameplay);
    }
}
