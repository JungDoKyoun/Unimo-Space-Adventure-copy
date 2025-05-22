using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGatheringDelay", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatheringDelay")]
public class GatheringDelay : BuildEffect
{
    public float gatheringDelay;
    public float gatheringDelayPercent;

    public override void ApplyBuildEffect()
    {
        playerStatus.gatheringDelay += gatheringDelay;
    }

    public override void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }

}
