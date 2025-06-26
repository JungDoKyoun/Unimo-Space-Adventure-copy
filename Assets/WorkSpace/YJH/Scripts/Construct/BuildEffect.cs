using UnityEngine;

public class BuildEffect : ScriptableObject, IStatModifier
{
    public virtual float ReturnFinalStat(float baseStat)
    {
        return baseStat;
    }
}