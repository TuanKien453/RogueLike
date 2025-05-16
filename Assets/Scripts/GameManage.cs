using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{

    public StatusEffectComponent statusEffectManager;
    public StatsComponent statsManager;
    [SerializeField]
    public StatusEffect statusToApply;
    public void TestFuntion()
    {
        statusEffectManager.AddStatus(statusToApply,0,1);
        Debug.Log(statsManager.GetStats(StatType.MaxHealth));
    }
    public void Start()
    {
        Debug.Log(statsManager.GetStats(StatType.MaxHealth));
    }
    void Update()
    {
        
    }
}
