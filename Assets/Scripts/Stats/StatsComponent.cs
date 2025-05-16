using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[RequireComponent(typeof(EntityStat))]
public class StatsComponent : MonoBehaviour
{
    private EntityStat statComponent;
    private List<IStatsModifier> modifiers = new List<IStatsModifier>();
    private void Awake()
    {
        statComponent = GetComponent<EntityStat>();
    }

    public void AddModifier(IStatsModifier modifier)
    {
        modifiers.Add(modifier);
    }

    public void RemoveModifier(IStatsModifier modifier)
    {
        modifiers.Remove(modifier);
    }

    public float GetStats(StatType st)
    {
        float baseValue = statComponent.Stats.GetStat(st);
        foreach (var mod in modifiers)
        {
            mod.ModifyStat(st,ref baseValue);
        }
        return baseValue;
    }
}
