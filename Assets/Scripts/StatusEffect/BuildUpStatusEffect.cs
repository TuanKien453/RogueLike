using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildUpStatusEffect : StatusEffect
{
    public float MaxBuildUp;


    [System.NonSerialized] public float CurrentBuildUp;


    public void AddBuildUp(float value)
    {
        CurrentBuildUp += value;

        if (CurrentBuildUp >= MaxBuildUp)
        {
            int extraStacks = (int)(CurrentBuildUp / MaxBuildUp);
            int canAdd = MaxStack - Stack;

            int added = Mathf.Min(extraStacks, canAdd);
            IncreaseStack(added);
            CurrentBuildUp -= added * MaxBuildUp;

            if (Stack >= MaxStack)
            {
                CurrentBuildUp = Mathf.Min(CurrentBuildUp, MaxBuildUp - 0.01f);
            }
        }
    }
}
