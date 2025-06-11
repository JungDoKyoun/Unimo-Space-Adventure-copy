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
    //    Type type = Type.GetType(className);//���ӽ����̽��� ������
    //    if (type == null)//���ӽ����̽��� ���ų� Ŭ������ ������
    //    {
    //        type = AppDomain.CurrentDomain.GetAssemblies()//��������� ��������
    //            .SelectMany(a => a.GetTypes())
    //            .FirstOrDefault(t => t.FullName == className);
    //        target = Activator.CreateInstance(type);
    //        if (type == null)//����������� ������
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
