using OpenCover.Framework.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class IncreaseStartGold2 : IUtilityBuildEffect
{
    public object Target { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string MethodName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Action IUtilityBuildEffect()
    {
        //Type a = Activator.CreateInstance(Method);
        return new Action(() => IncreaseStartGold());
    }
    private void IncreaseStartGold()
    {
        FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(100);
    }
    /*
    public void Execute(string methodName)
    {
        MethodInfo method = this.GetType().GetMethod(methodName);
        if (method != null && method.GetParameters().Length == 0)
        {
            method.Invoke(this, null);
        }
        else
        {
            Console.WriteLine($"No method found or invalid signature for '{methodName}'");
        }
    }
    */
}
