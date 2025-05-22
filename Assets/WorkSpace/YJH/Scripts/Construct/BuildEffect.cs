using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildEffect :ScriptableObject
{
    public PlayerStatus playerStatus= new PlayerStatus();
    public virtual void ApplyBuildEffect()
    {

    }
    public virtual void SetPlayerStatus(PlayerStatus status)
    {

    }



}
