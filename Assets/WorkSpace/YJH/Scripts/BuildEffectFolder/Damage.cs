using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectDamage")]
public class Damage : BuildEffect
{
    public float damage;
    public float damagePercent;
    
    
    public override float ReturnFinalStat(float baseStat)
    {
        return baseStat * damagePercent / 100 + damage;
    }
}
