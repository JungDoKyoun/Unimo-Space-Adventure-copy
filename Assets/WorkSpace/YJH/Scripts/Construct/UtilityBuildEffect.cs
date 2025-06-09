using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using YJH;



[CreateAssetMenu(fileName = "UtilityBuildEffect", menuName = "ScriptableObject/ConstructEffect/UtilityBuildEffect")]
public class UtilityBuildEffect : ScriptableObject, IUtilityBuildEffect
{
    [SerializeField] object target;

    [SerializeField] string className="YJH.MethodCollection";
    [SerializeField] string methodName;
    

    public object Target { get { return target; } set {target=value ; } }
    public string ClassName { get { return className; } set { className = value; } }
    public string MethodName { get {return methodName; } set {methodName=value ; } }
    

    public void Excute(object targetScript=null, string methodString="")
    {
        if (targetScript == null && methodString == "")
        {
            
            
            
                MethodInfo method = typeof(YJH.MethodCollection).GetMethod(methodName);

                if (method == null)
                {
                    Debug.Log("noMethod");
                }
                else
                {
                    method.Invoke(null,null);
                }
            

        }
        else
        {
            
                MethodInfo method = targetScript.GetType().GetMethod(methodString);

            if(method.GetParameters().Length != 0)
            {
                Debug.Log("you can't use non void method");
                return;
            }
                method.Invoke(targetScript, null);
           
        }
    }

    //public void SetTarget()
    //{
    //    Type type = Type.GetType(className);//네임스페이스가 있으면
    //    if (type == null)//네임스페이스가 없거나 클래스가 없으면
    //    {
    //        type = AppDomain.CurrentDomain.GetAssemblies()//어셈블리에서 가져오기
    //            .SelectMany(a => a.GetTypes())
    //            .FirstOrDefault(t => t.FullName == className);
    //        target = Activator.CreateInstance(type);
    //        if (type == null)//어셈블리에서도 없으면
    //        {
    //            Debug.Log("wrong class name");
    //            return;
    //        }
    //    }
    //    else
    //    {
    //        target = Activator.CreateInstance(type);
    //    }
    //    
    //
    //     
    //
    //}
}
