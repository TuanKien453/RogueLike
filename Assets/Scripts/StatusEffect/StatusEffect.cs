using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect : ScriptableObject
{
    public string Id;
    public string EffectName;
    public bool IsDebuff;
    public float Duration;
    public int Priority;
    public int MaxStack;


    [System.NonSerialized] public float ElapsedTime=0f;
    [System.NonSerialized] public int Stack=0;


    private Dictionary<Type, Component> cacheComponent = new();
    protected T GetCachedComponent<T>(GameObject target) where T : Component
    {
        var type = typeof(T);

        if (!cacheComponent.TryGetValue(type, out var comp))
        {
            comp = target.GetComponent<T>();
            cacheComponent[type] = comp;

            if (comp == null)
            {
                Debug.LogError($"[StatusEffect] Component of type {type.Name} not found on {target.name}", target);
            }
        }

        return comp as T;
    }

    public void IncreaseStack(int stackIncrease)
    {
        Stack = Mathf.Min(Stack + stackIncrease, MaxStack);
    }

    public abstract void OnApply(GameObject target);
    public abstract void OnRemove(GameObject target);
    public abstract void OnTick(GameObject target, float deltaTime);

    protected virtual void OnEnable()
    {
        if (string.IsNullOrEmpty(Id))
        {
            Id = System.Guid.NewGuid().ToString();
        }
    }

    public override bool Equals(object obj)
    {
        return obj is StatusEffect effect && effect.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}