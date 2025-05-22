using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeGatherRange", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatherRange")]
public class ItemDetectionRange : BuildEffect
{
    public float gatheringRange;
    public float gatheringRangePercent;

    public override void ApplyBuildEffect()
    {
        playerStatus.itemDetectionRange += gatheringRange;
        if (playerStatus.itemDetectionRange < 0)
        {
            playerStatus.itemDetectionRange = 0;
        }
    }
    public override void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }


}
