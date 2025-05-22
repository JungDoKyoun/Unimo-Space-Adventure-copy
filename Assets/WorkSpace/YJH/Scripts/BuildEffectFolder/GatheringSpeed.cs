using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGatheringSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatheringSpeed")]
public class GatheringSpeed : BuildEffect
{
    public float gatheringSpeed;
    public float gatheringSpeedPercent;

    

    
    public override float ReturnFinalStat(float baseStat)
    {
        return baseStat * gatheringSpeedPercent / 100 + gatheringSpeed;
    }
}
