using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityStat))]
public abstract class BaseEntity : MonoBehaviour, IHealth, IDamageable
{
    public float CurrentHP { get; set; }
    public bool IsDead => CurrentHP <= 0;

    public abstract void Die();

    public abstract void TakeDamage(float amount, GameObject attacker);
}
