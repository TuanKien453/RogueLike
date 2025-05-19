using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum StatType
{
    MaxHealth,
    Damage,
    Defense,
    MoveSpeed,
    JumpForce
}
[CreateAssetMenu(fileName = "New StatsSO", menuName = "Customs/StatsSO")]
public class StatsSO : ScriptableObject
{
    public float Damage;
    public float MoveSpeed;
    public float MaxHealth;
    public float Defense;
    public float JumpForce;
    public float GetStat(StatType type)
    {
        return type switch
        {
            StatType.MaxHealth => MaxHealth,
            StatType.Damage => Damage,
            StatType.Defense => Defense,
            StatType.MoveSpeed => MoveSpeed,
            StatType.JumpForce => JumpForce,
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown stat type: {type}")
        };
    }
}