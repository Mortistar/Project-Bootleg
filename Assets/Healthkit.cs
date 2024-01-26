using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Healthkit : MonoBehaviour
{
    [SerializeField] private EventReference healthkitRef;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript.GetHealable()) 
            {
                //RuntimeManager.PlayOneShot(healthkitRef, transform.position);
                playerScript?.RestoreHealth(50f);
                Destroy(gameObject);
            }
        }
    }
}