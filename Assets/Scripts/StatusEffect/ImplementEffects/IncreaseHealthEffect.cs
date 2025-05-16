using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Customs/Status Effects/Increase Health Effect", fileName = "IncreaseHealthEffect")]
public class IncreaseHealthEffect : StatusEffect, IStatsModifier
{
    public float healthToIncrease;

    public void ModifyStat(StatType stat, ref float currentValue)
    {
        if(stat == StatType.MaxHealth)
        currentValue += healthToIncrease*Stack;
    }

    public override void OnApply(GameObject target)
    {
        Debug.Log("apply increase Health");
    }

    public override void OnRemove(GameObject target)
    {
        Debug.Log("remove increase Health");
    }

    public override void OnTick(GameObject target, float deltaTime)
    {

    }
}
