using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IUtilityBuildEffect
{
    System.Object Target {  get; set; }
    string MethodName {  get; set; }
    string ClassName {  get; set; }
    public void SetTarget();
    public void Excute(System.Object target, string methodName);
    


}
