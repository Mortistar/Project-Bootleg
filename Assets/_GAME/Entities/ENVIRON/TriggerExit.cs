using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerExit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (LevelManager.instance.GetLevelIndex() == LevelManager.LevelIndex.Credits)
            {
                GameManager.instance.Quit(1f);
                Destroy(gameObject);
            }else
            {
                GameManager.instance.CompleteLevel();
                Destroy(gameObject);
            }
            
        }
    }
}