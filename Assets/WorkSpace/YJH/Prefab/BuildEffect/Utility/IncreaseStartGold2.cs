using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStartGold2 : IUtilityBuildEffect
{
    public Action IUtilityBuildEffect()
    {

        return new Action(() => IncreaseStartGold());
    }
    private void IncreaseStartGold()
    {
        FirebaseDataBaseMgr.Instance.UpdateRewardIngameCurrency(100);
    }
}
