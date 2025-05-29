using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStartGold1 : UtilityBuildBase
{
   
    
    public override Delegate IUtilityBuildEffect()
    {



        return new Action(()=>IncreaseStartGold());
    }
    private void IncreaseStartGold()
    {
        FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(50);
    }




}
