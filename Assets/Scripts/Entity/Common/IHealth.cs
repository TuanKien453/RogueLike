using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    float CurrentHP { get; set; }
    void Die();
    bool IsDead { get; }
}