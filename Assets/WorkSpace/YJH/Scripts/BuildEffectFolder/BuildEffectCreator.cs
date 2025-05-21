using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectSpeed")]
public class Speed :BuildEffect
{
    public float speed;
    public override void ApplyBuildEffect()
    {
        playerStatus.moveSpeed += speed;
    }
}

[CreateAssetMenu(fileName = "ChangeSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectDamage")]
public class Damage : BuildEffect
{
    public float damage;
    public override void ApplyBuildEffect()
    {
        playerStatus.playerDamage += damage;
    }
}
[CreateAssetMenu(fileName = "ChangeMaxHP", menuName = "ScriptableObject/ConstructEffect/BuildEffectMaxHp")]
public class MaxHp : BuildEffect
{
    public float hp;

    public override void ApplyBuildEffect()
    {
        playerStatus.maxHP += hp;
    }
}


[CreateAssetMenu(fileName = "ChangeGatherRange", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatherRange")]
public class ItemDetectionRange : BuildEffect
{
    public float gatheringRange;

    public override void ApplyBuildEffect()
    {
        playerStatus.itemDetectionRange += gatheringRange;
        if(playerStatus.itemDetectionRange < 0)
        {
            playerStatus.itemDetectionRange= 0; 
        }
    }



}


[CreateAssetMenu(fileName = "ChangeGatheringSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatheringSpeed")]
public class GatheringSpeed: BuildEffect
{
    public float gatheringSpeed;

    public override void ApplyBuildEffect()
    {
        playerStatus.gatheringSpeed += gatheringSpeed;
    }



}


[CreateAssetMenu(fileName = "ChangeGatheringDelay", menuName = "ScriptableObject/ConstructEffect/BuildEffectGatheringDelay")]
public class GatheringDelay : BuildEffect
{
    public float gatheringDelay;

    public override void ApplyBuildEffect()
    {
        playerStatus.gatheringDelay += gatheringDelay;
    }



}
