using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public enum LevelIndex
    {
        Logo,
        Intro,
        MainMenu,
        Tutorial,
        Dungeon,
        Credits
    }
    //UI transition speed
    [SerializeField] private float transitionSpeed = 1;
    bool isFastFade;
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
    }
    void Start()
    {
        if (isFastFade)
        {
            UIManager.instance.FadeIn(transitionSpeed / 2);
        }else
        {
            UIManager.instance.FadeIn(transitionSpeed);
        }
    }
    //On scene load (Functions the same as Start() for persistent instance)
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Start();
    }
    public void LevelLoad(LevelIndex levelToLoad)
    {
        InputManager.instance.DisableControls();
        isFastFade = false;
        StartCoroutine(iLevelLoad(levelToLoad));
    }
    private IEnumerator iLevelLoad(LevelIndex levelToLoad)
    {
        UIManager.instance.FadeOut(transitionSpeed);
        yield return new WaitForSeconds(transitionSpeed);
        SceneManager.LoadScene((int)levelToLoad);
        yield return null;
    }
    public void LevelLoadFast(LevelIndex levelToLoad)
    {
        InputManager.instance.DisableControls();
        isFastFade = true;
        StartCoroutine(iLevelLoadFast(levelToLoad));
    }
    private IEnumerator iLevelLoadFast(LevelIndex levelToLoad)
    {
        UIManager.instance.FadeOut(transitionSpeed / 2);
        yield return new WaitForSeconds(transitionSpeed / 2);
        SceneManager.LoadScene((int)levelToLoad);
        yield return null;
    }
}
