using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGatherRange", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatherRange")]
public class ItemDetectionRange : BuildEffect
{
    public float gatheringRange;
    public float gatheringRangePercent;

    
    
    public override float ReturnFinalStat(float baseStat)
    {
        return baseStat * gatheringRangePercent / 100 + gatheringRange;
    }

}
