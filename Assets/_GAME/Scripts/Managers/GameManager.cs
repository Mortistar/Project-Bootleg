using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    //PERSISTENCE
    public static GameManager instance;

    //GLOBAL Delegate
    public delegate void gameEvent();

    //Pause Delegates
    public gameEvent Pause;
    public gameEvent UnPause;

    //GLOBAL STATS

    //Implement settings data
    public PlayerStats playerData {get; private set;}
    //Implement Dungeon data
    public DungeonStats dungeonData {get; private set;}
    public LevelManager.LevelIndex currentLevel {get; private set;}
    
    void Awake()
    {
        PersistenceCheck();
    }
    void Update()
    {
        if (dungeonData != null)
        {
            dungeonData.AddTime(Time.deltaTime);
        }
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
        playerData = null;
        dungeonData = null;
        currentLevel = LevelManager.LevelIndex.MainMenu;
    }
    public void SetPaused(bool isPause)
    {
        if (isPause)
        {
            Pause?.Invoke();
        }else
        {
            UnPause?.Invoke();
        }
    }
    public void SetPlayerData(PlayerStats data)
    {
        playerData = data;
    }
    public void InitialiseDungeon()
    {
        dungeonData = new DungeonStats();
    }
    public void InitialiseDungeon(int secretCount)
    {
        dungeonData = new DungeonStats(secretCount);
    }
    public void CompleteLevel()
    {
        LevelManager.instance.LevelLoadFast(LevelManager.LevelIndex.LevelResults);
    }
    public void AdvanceLevel()
    {
        dungeonData = null;
        currentLevel++;
        LevelManager.instance.LevelLoad(currentLevel);
    }
    public void FailLevel()
    {
        Time.timeScale = 1f;
        dungeonData = null;
        LevelManager.instance.LevelLoadFast(currentLevel);
    }
    //Quit game with custom time
    public void Quit(float timeToQuit)
    {
        StartCoroutine(iQuit(timeToQuit));
    }
    private IEnumerator iQuit(float timeToQuit)
    {
        UIManager.instance.FadeOut(timeToQuit);
        yield return new WaitForSeconds(timeToQuit);
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
        yield return null;
    }
}
