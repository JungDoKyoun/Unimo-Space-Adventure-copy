using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeMaxHP", menuName = "ScriptableObject/ConstructEffect/BuildEffectMaxHp")]
public class MaxHp : BuildEffect
{
    public float hp;
    public float hpPercent;

    public override void ApplyBuildEffect()
    {
        playerStatus.maxHP += hp;
    }
    public override void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }
}
