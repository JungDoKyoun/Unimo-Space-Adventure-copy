using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeMaxHP", menuName = "ScriptableObject/ConstructEffect/BuildEffectMaxHp")]
public class MaxHp : BuildEffect
{
    public float hp;
    public float hpPercent;

    
    
    public override float ReturnFinalStat(float baseStat)
    {
        return baseStat * hpPercent / 100 + hp;
    }
}
