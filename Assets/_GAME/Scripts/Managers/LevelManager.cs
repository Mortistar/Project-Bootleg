using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public enum LevelIndex
    {
        Logo = 0,
        Intro = 1,
        MainMenu = 2,
        Tutorial = 3,
        DungeonOne = 4,
        DungeonTwo = 5,
        DungeonThree = 6,
        Credits = 7,
        LevelPreview = 8,
        LevelResults = 9
    }
    //UI transition speed
    [SerializeField] private float transitionSpeed = 1;
    private bool isFastFade;
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
        SceneManager.sceneLoaded += OnSceneLoaded;
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
    public void LevelLoadInstant(LevelIndex levelToLoad)
    {
        InputManager.instance.DisableControls();
        isFastFade = true;
        UIManager.instance.FadeOut(0.1f);
        SceneManager.LoadScene((int)levelToLoad);
    }
    public LevelIndex GetLevelIndex()
    {
        return (LevelIndex)SceneManager.GetActiveScene().buildIndex;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
