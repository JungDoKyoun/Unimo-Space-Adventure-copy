using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGatheringObject 
{
    public float NowHP
    {
        get;
        set;
    }
    public float MaxHP
    {
        get;

    }
    public int ActiveCount
    {
        get; set;
    }
    public void UseItem();

    public void BeGathering();

    public IGatheringObject ReturnSelf();


    public void OnGatheringEnd();
    
}
