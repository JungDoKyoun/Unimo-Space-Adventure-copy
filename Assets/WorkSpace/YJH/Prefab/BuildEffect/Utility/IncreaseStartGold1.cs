using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStartGold1 : IUtilityBuildEffect
{
    public object Target { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string MethodName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Action IUtilityBuildEffect()
    {

        return new Action(()=>IncreaseStartGold());
    }
    private void IncreaseStartGold()
    {
        FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(50);
    }




}
