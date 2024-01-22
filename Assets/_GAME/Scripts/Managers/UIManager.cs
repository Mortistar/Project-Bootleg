using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup transitionFade;
    [SerializeField] private CanvasGroup pauseMenuGroup;
    [SerializeField] private CanvasGroup optionsMenuGroup;

    public static UIManager instance;

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
        //In case I've done fucky stuff with menu visibility on the prefab
        transitionFade.alpha = 1;
        pauseMenuGroup.alpha = 0;
        optionsMenuGroup.alpha = 0;

        InputManager.instance.controls.Gameplay.Menu.performed += OpenPauseMenu;
    }
    /// <summary>
    /// Opens the in-game pause menu
    /// </summary>
    public void OpenPauseMenu(InputAction.CallbackContext ctx)
    {
        InputManager.instance.controls.Gameplay.Menu.performed -= OpenPauseMenu;
        InputManager.instance.controls.Gameplay.Menu.performed += ClosePauseMenu;
        pauseMenuGroup.alpha = 1;
        pauseMenuGroup.blocksRaycasts = true;
        pauseMenuGroup.interactable = true;
        GameManager.instance.SetPaused(true);
    }
    /// <summary>
    /// Closes the in-game pause menu
    /// </summary>
    public void ClosePauseMenu(InputAction.CallbackContext ctx)
    {
        InputManager.instance.controls.Gameplay.Menu.performed += OpenPauseMenu;
        InputManager.instance.controls.Gameplay.Menu.performed -= ClosePauseMenu;
        pauseMenuGroup.alpha = 0;
        pauseMenuGroup.blocksRaycasts = false;
        pauseMenuGroup.interactable = false;
        GameManager.instance.SetPaused(false);
    }
    /// <summary>
    /// Opens the in-game options menu
    /// </summary>
    public void OpenOptionsMenu()
    {
        optionsMenuGroup.alpha = 1;
        optionsMenuGroup.blocksRaycasts = true;
        optionsMenuGroup.interactable = true;
        GameManager.instance.SetPaused(true);
    }
    /// <summary>
    /// Closes the in-game options menu
    /// </summary>
    public void CloseOptionsMenu()
    {
        optionsMenuGroup.alpha = 0;
        optionsMenuGroup.blocksRaycasts = false;
        optionsMenuGroup.interactable = false;
        GameManager.instance.SetPaused(false);
    }
    public void FadeOut(float time)
    {
        transitionFade.blocksRaycasts = true;
        transitionFade.DOFade(1, time);
    }
    public void FadeIn(float time)
    {
        transitionFade.blocksRaycasts = false;
        transitionFade.DOFade(0, time);
    }
}
