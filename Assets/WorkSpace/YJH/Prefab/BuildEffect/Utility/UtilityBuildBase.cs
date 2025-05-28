using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityBuildBase : MonoBehaviour,IUtilityBuildEffect
{
    public virtual Delegate IUtilityBuildEffect()
    {
        return null;
    }

    
    
}
