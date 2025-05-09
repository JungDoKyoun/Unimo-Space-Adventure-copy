using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGatheringObject 
{
    public int ActiveCount
    {
        get; set;
    }
    public void UseItem()
    {

    }
    public void BeGathering()
    {

    }
    public IGatheringObject ReturnSelf()
    {
        return this;
    }

    public void OnGatheringEnd()
    {

    }
}
