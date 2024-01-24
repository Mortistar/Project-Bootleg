using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHips : MonoBehaviour, IDamageable, IKickable, ISweepable
{
    public void TakeDamage(float damage)
    {
        transform.root.GetComponent<IDamageable>()?.TakeDamage(damage);
    }
    public void Kick(float damage, Vector3 direction)
    {
        transform.root.GetComponent<IKickable>()?.Kick(damage, direction);
    }
    public void Sweep(Vector3 direction)
    {
        transform.root.GetComponent<ISweepable>()?.Sweep(direction);
    }
}
