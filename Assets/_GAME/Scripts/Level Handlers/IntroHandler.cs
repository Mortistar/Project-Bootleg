using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMODUnityResonance;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IntroHandler : MonoBehaviour
{
    [SerializeField] private EventReference introMusicRef;

    [SerializeField] private Image slideshow;
    [SerializeField] private TMPro.TextMeshProUGUI typewriter;

    [SerializeField] private Sprite[] bgSprites;
    [SerializeField] private string[] text;

    [SerializeField] private float typeSpeed;
    [SerializeField] private float waitSpeed;
    private int textIndex;
    private FMOD.Studio.EventInstance musicInst;

    void Start()
    {
        InputManager.instance.SetControls(InputManager.ControlType.UI);
        InputManager.instance.controls.UI.Continue.performed += Skip;
        musicInst = RuntimeManager.CreateInstance(introMusicRef);
        musicInst.start();
        AudioManager.instance.QueueSong(AudioManager.Song.MainMenu);
        textIndex = 0;
        StartCoroutine(IType());
    }

    private IEnumerator IType()
    {   
        slideshow.sprite = bgSprites[textIndex];
        typewriter.text = "";
        foreach(char c in text[textIndex])
        {
            yield return new WaitForSeconds(typeSpeed);
            typewriter.text += c;
        }
        yield return new WaitForSeconds(waitSpeed);
        textIndex++;
        if (textIndex >= text.Length)
        {
            LoadMenu();
        }else
        {
            StartCoroutine(IType());
        }
    }
    private void Skip(InputAction.CallbackContext ctx)
    {
        StopAllCoroutines();
        LevelManager.instance.LevelLoadInstant(LevelManager.LevelIndex.MainMenu);
    }
    private void LoadMenu()
    {
        LevelManager.instance.LevelLoadInstant(LevelManager.LevelIndex.MainMenu);
    }
    void OnDisable()
    {
        InputManager.instance.controls.UI.Continue.performed -= Skip;
        musicInst.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
