using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGatheringDelay", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatheringDelay")]
public class GatheringDelay : BuildEffect
{
    public float gatheringDelay;
    public float gatheringDelayPercent;

   

    
    public override float ReturnFinalStat(float baseStat)
    {
        return baseStat * gatheringDelayPercent / 100 + gatheringDelay;
    }
}
