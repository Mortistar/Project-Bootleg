using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ILogo());
    }

    private IEnumerator ILogo()
    {
        yield return new WaitForSeconds(1f);
        //Play sound
        yield return new WaitForSeconds(1f);
        LevelManager.instance.LevelLoad(LevelManager.LevelIndex.Intro);
    }
}