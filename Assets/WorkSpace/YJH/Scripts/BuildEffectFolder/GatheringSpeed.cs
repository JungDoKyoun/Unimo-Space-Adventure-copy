using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGatheringSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatheringSpeed")]
public class GatheringSpeed : BuildEffect
{
    public float gatheringSpeed;
    public float gatheringSpeedPercent;

    public override void ApplyBuildEffect()
    {
        playerStatus.gatheringSpeed += gatheringSpeed;
    }

    public override void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }

}
