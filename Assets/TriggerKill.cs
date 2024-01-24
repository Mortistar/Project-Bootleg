using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerKill : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        IDamageable damageScript = other.GetComponent<IDamageable>();
        if (damageScript != null)
        {
            damageScript.TakeDamage(9999f);
        }
    }
}
