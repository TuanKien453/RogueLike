using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
[RequireComponent(typeof(EntityStat))]
public class StatsComponent : MonoBehaviour
{
    private EntityStat statComponent;
    private List<IStatsModifier> modifiers = new List<IStatsModifier>();

    private Dictionary<StatType,float> cacheStats = new Dictionary<StatType,float>();
    private void Awake()
    {
        statComponent = GetComponent<EntityStat>();
    }

    private void Update()
    {
        cacheStats.Clear();
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
        if (cacheStats.TryGetValue(st,out float exitValue))
        {
            return exitValue;
        }

        float baseValue = statComponent.Stats.GetStat(st);
        foreach (var mod in modifiers)
        {
            mod.ModifyStat(st,ref baseValue);
        }
        cacheStats.Add(st,baseValue);

        return baseValue;
    }
}
