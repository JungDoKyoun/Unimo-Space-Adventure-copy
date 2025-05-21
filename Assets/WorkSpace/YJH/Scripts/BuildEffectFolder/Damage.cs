using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectDamage")]
public class Damage : BuildEffect
{
    public float damage;
    public float damagePercent;
    public override void ApplyBuildEffect()
    {
        playerStatus.playerDamage += damage;
    }
    public override void SetPlayerStatus(PlayerStatus status)
    {
        playerStatus = status;
    }
}
