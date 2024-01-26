using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private EventReference keyPickupRef;
    [SerializeField] private Key.KeyType keyType;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //play sound
            //RuntimeManager.PlayOneShot(keyPickupRef)
            other.GetComponent<Player>().AddKey(keyType);
            Destroy(gameObject);
        }
    }
}
