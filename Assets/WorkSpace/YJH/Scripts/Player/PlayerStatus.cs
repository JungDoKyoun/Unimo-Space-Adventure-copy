using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerStatus 
{
    public float currentHealth ;
    public float maxHP;
    public float playerDamage ;
    public float itemDetectionRange;
    public float gatheringSpeed;
    public float gatheringDelay;
    public float moveSpeed;
    
    public PlayerStatus Clone()
    {
        PlayerStatus clone = new PlayerStatus();
        clone.currentHealth=currentHealth;
        clone.maxHP = maxHP;
        clone.playerDamage = playerDamage;
        clone.itemDetectionRange = itemDetectionRange;
        clone.gatheringSpeed = gatheringSpeed;
        clone.gatheringDelay = gatheringDelay;
        clone.moveSpeed = moveSpeed;
        return clone;
    }
    //최종속도
    //public float baseSpeed;





}
