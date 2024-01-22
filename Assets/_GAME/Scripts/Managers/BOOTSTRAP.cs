using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BOOTSTRAP : MonoBehaviour
{
    public static BOOTSTRAP instance;

    //MANAGER REFERENCE
    [SerializeField] private GameObject managerGame;
    [SerializeField] private GameObject managerLevel;
    [SerializeField] private GameObject managerInput;
    [SerializeField] private GameObject managerAudio;
    [SerializeField] private GameObject managerUI;

    //PERSISTENCE
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

    //Initialisation script (ON GAME START -> SHOULD ONLY BE CALLED ONCE)
    private void INIT()
    {
        //Initialise tweening engine
        DOTween.Init();
        //Set default tween type
        DOTween.defaultEaseType = Ease.Linear;
        //Should be affected by Timescale
        DOTween.defaultTimeScaleIndependent = false;

        //Create Managers
        Instantiate(managerGame);
        Instantiate(managerLevel);
        Instantiate(managerInput);
        Instantiate(managerAudio);
        Instantiate(managerUI);
    }
}
