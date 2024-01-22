using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    //Implement Player data
    //Implement Dungeon data
    
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
}
