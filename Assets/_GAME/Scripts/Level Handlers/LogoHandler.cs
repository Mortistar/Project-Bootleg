using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class LogoHandler : MonoBehaviour
{
    [SerializeField] private bool isPhysical;
    [SerializeField] private Image imgContinue;
    // Start is called before the first frame update
    void Start()
    {
        if (!isPhysical)
        {
            StartCoroutine(ILogo());
            return;
        }
        imgContinue.enabled = true;
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        InputManager.instance.controls.UI.Continue.performed += Continue;
    }

    private IEnumerator ILogo()
    {
        yield return new WaitForSeconds(1f);
        //Play sound
        yield return new WaitForSeconds(1f);
        LevelManager.instance.LevelLoad(LevelManager.LevelIndex.Intro);
    }

    private void Continue(InputAction.CallbackContext ctx)
    {
        InputManager.instance.controls.UI.Continue.performed -= Continue;
        LevelManager.instance.LevelLoad(LevelManager.LevelIndex.Intro);
    }
}