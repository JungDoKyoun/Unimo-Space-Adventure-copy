using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus 
{
    private float playerHP;
    private float playerMoveSpeed;
    private float playerGatheringRadius;

    public float PlayerHP
    {
        get {  return playerHP; }
        set 
        {
            if (value < 0)
            {
                playerHP = 0;
            }
            else
            {
                playerHP = value;
            }
        }
    }
    public float PlayerMoveSpeed
    {
        get { return playerMoveSpeed; }
        set { playerGatheringRadius = value; }
    }

    public float PlayerGatheringRadius
    {
        get { return playerGatheringRadius; }
    }



   
}
