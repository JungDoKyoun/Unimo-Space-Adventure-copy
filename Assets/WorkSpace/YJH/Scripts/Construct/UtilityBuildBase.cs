using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstructInfo", menuName = "ScriptableObject/UtilityConstruct")]
public class UtilityBuildBase : ConstructBase
{
    [SerializeField] UtilityBuildEffect buildEffect;
    public UtilityBuildEffect UtilityBuildEffect { get { return buildEffect; } }
    
    
}
