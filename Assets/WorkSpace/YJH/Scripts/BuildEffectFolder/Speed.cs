using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectSpeed")]
public class Speed :BuildEffect
{
    public float speed;
    public float speedPercent;
    public override void ApplyBuildEffect()
    {
        playerStatus.moveSpeed += speed;
    }
    public override void SetPlayerStatus(PlayerStatus status)
    {
       playerStatus = status;
    }
}












