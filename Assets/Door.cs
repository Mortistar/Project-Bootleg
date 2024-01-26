using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Door : MonoBehaviour
{
    [SerializeField] private EventReference unlockRef;
    [SerializeField] private Key.KeyType lockType;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Player>().TryUnlock(lockType))
            {
                //play sound
                //RuntimeManager.PlayOneShot(unlockRef, transform.position);
                Destroy(gameObject);
            }
        }
    }
}
