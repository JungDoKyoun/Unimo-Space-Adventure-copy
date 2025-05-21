using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus 
{
    public float currentHealth ;
    public float maxHP;
    public float playerDamage ;
    public float itemDetectionRange;
    public float gatheringSpeed;
    public float gatheringDelay;
    public float moveSpeed;
    
    public void Duplicate(PlayerStatus clone)
    {
        currentHealth = clone.currentHealth;
        maxHP = clone.maxHP;
        playerDamage = clone.playerDamage;
        itemDetectionRange = clone.itemDetectionRange;
        gatheringSpeed = clone.gatheringSpeed;
        gatheringDelay = clone.gatheringDelay;
        moveSpeed = clone.moveSpeed;
            
    }
    //최종속도
    //public float baseSpeed;





}
