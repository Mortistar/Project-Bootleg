using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class SecretWall : MonoBehaviour, IKickable
{
    [SerializeField] private GameObject wallGibRef;
    [SerializeField] private string hint;
    [SerializeField] private EventReference wallRef;

    public void Kick(float damage, Vector3 direction)
    {
        GameObject wallgibs = Instantiate(wallGibRef);
        wallgibs.transform.position = transform.position;
        if (hint != "")
        {
            UIManager.instance.GiveHint(UIManager.HintType.Secret, hint);
        }
        GameManager.instance.dungeonData.FindSecret();
        RuntimeManager.PlayOneShot(wallRef);
        Destroy(gameObject);
    }
}
