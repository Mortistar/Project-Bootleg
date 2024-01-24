using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHips : MonoBehaviour, IDamageable
{
    [SerializeField] private Skeleton handler;
    public void TakeDamage(float damage)
    {
        handler.TakeDamage(damage);
    }
}
