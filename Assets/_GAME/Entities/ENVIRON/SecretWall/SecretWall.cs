using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWall : MonoBehaviour, IKickable
{
    [SerializeField] private GameObject wallGibRef;
    [SerializeField] private string hint;

    public void Kick(float damage, Vector3 direction)
    {
        GameObject wallgibs = Instantiate(wallGibRef);
        wallgibs.transform.position = transform.position;
        if (hint != "")
        {
            UIManager.instance.GiveHint(UIManager.HintType.Secret, hint);
        }
        GameManager.instance.dungeonData.FindSecret();
        Destroy(gameObject);
    }
}
