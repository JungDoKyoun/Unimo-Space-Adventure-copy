using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChangeSpeed", menuName = "ScriptableObject/ConstructEffect/BuildEffectSpeed")]
public class Speed :BuildEffect
{
    public float speed;
    public float speedPercent;
    
    
    public override float ReturnFinalStat(float baseStat)
    {
        return baseStat * speedPercent / 100 + speed;
    }
}












