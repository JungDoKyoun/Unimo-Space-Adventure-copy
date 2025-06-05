using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UtilityBuildEffect", menuName = "ScriptableObject/ConstructEffect/UtilityBuildEffect")]
public class UtilityBuildEffect : ScriptableObject, IUtilityBuildEffect
{
    [SerializeField] object target;
    [SerializeField] string methodName;
    public object Target { get { return target; } set {target=value ; } }
    public string MethodName { get {return methodName; } set {methodName=value ; } }

    public Action IUtilityBuildEffect()
    {
        return null;
    }
}
