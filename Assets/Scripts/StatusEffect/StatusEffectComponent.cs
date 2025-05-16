using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(StatsComponent))]
public class StatusEffectComponent : MonoBehaviour
{
    private StatsComponent StatManager;

    private Dictionary<string, StatusEffect> _activeStatusMap = new();

    void Awake()
    {
        StatManager = GetComponent<StatsComponent>();
    }

    public void AddStatus(StatusEffect status, float buildUp = 0f, int stack = 0)
    {
        string key = status.Id;

        if (_activeStatusMap.TryGetValue(key, out var existing))
        {
            if (existing is BuildUpStatusEffect buildUpEffect)
            {
                buildUpEffect.AddBuildUp(buildUp);
            }
            existing.ElapsedTime = 0f;
            existing.IncreaseStack(stack);
        }
        else
        {
            var effectInstance = Instantiate(status);
            effectInstance.IncreaseStack(stack);
            if (effectInstance is BuildUpStatusEffect buildUpEffect)
            {
                buildUpEffect.AddBuildUp(buildUp);
            }

            _activeStatusMap.Add(effectInstance.Id, effectInstance);
            effectInstance.OnApply(gameObject);

            if (effectInstance is IStatsModifier modifier)
            {
                StatManager?.AddModifier(modifier);
            }

        }
    }

    public void RemoveStatus(StatusEffect effect)
    {
        if (_activeStatusMap.Remove(effect.Id))
        {
            effect.OnRemove(gameObject);

            if (effect is IStatsModifier modifier)
            {
                StatManager?.RemoveModifier(modifier);
            }
        }
    }

    void Update(){
        float deltaTime = Time.deltaTime;
        List<StatusEffect> toRemove = new();

        foreach (var kvp in _activeStatusMap)
        {
            var effect = kvp.Value;

            effect.ElapsedTime += deltaTime;
            effect.OnTick(gameObject, deltaTime);

            if (effect.ElapsedTime >= effect.Duration)
            {
                toRemove.Add(kvp.Value);
            }
        }

        foreach (var item in toRemove)
        {
            RemoveStatus(item);
        }
    }
}
