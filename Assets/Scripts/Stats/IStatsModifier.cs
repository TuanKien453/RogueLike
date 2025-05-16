using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsModifier
{
    void ModifyStat(StatType stat,ref float currentValue);
}
