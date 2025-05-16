using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseEntity
{
    public override void Die()
    {
        Debug.Log("Player die");
    }

    public override void TakeDamage(float amount, GameObject attacker)
    {
        Debug.Log("Player TakeDamage");
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
