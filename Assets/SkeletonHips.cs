using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHips : MonoBehaviour, IDamageable
{
    private IDamageable handler;
    void Awake()
    {
        handler = transform.root.GetComponent<IDamageable>();
    }
    public void TakeDamage(float damage)
    {
        handler.TakeDamage(damage);
    }
}
