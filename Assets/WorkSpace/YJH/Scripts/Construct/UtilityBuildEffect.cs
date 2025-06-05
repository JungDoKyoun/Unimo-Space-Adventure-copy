using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



[CreateAssetMenu(fileName = "UtilityBuildEffect", menuName = "ScriptableObject/ConstructEffect/UtilityBuildEffect")]
public class UtilityBuildEffect : ScriptableObject, IUtilityBuildEffect
{
    [SerializeField] object target;
    [SerializeField] string methodName;
    [SerializeField] string className;
    public object Target { get { return target; } set {target=value ; } }
    public string MethodName { get {return methodName; } set {methodName=value ; } }
    public string ClassName { get {return className; } set {className=value ; } }

    public void Excute(object targetScript=null, string methodString="")
    {
        if (target == null && methodName == "")
        {
            if (this.target == null)
            {
                return;
            }
            else
            {
                MethodInfo method = target.GetType().GetMethod(methodName);
                method.Invoke(this.target,null);
            }

        }
        else
        {
            MethodInfo method = targetScript.GetType().GetMethod(methodString);
            method.Invoke(targetScript, null);
        }
    }

    public void SetTarget()
    {
        Type type = Type.GetType(className);
        target = Activator.CreateInstance(type);
        
    }
}
