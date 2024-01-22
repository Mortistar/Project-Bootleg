using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public enum ControlType
    {
        UI,
        Gameplay
    }
    public static InputManager instance;
    
    public Controls controls {get; private set;}

    void Awake()
    {
        PersistenceCheck();
    }
    private void PersistenceCheck()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            INIT();
        }else
        {
            Destroy(this.gameObject);
        }
    }
    private void INIT()
    {
        //Initialisation

        GameManager.instance.Pause += OnPaused;
        GameManager.instance.UnPause += OnUnPaused;

        //Controls
        controls = new Controls();
        controls.Enable();
        SetControls(ControlType.UI);
        
        
    }
    public void SetControls(ControlType type)
    {
        if (type == ControlType.UI)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            controls.UI.Enable();
            controls.Gameplay.Disable();
        }else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controls.UI.Disable();
            controls.Gameplay.Enable();
        }
    }
    public void DisableControls()
    {
        controls.Disable();
    }
    void OnPaused()
    {
        InputSystem.PauseHaptics();
    }
    void OnUnPaused()
    {
        InputSystem.ResumeHaptics();
    }
    void OnDisable()
    {
        GameManager.instance.Pause -= OnPaused;
        GameManager.instance.UnPause -= OnUnPaused;
    }
}
