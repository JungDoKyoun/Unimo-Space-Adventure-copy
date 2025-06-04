using System;

[Serializable]

public class PlayerStatus
{
    public float currentHealth;

    public float maxHP;

    public float playerDamage;

    public float itemDetectionRange;

    public float gatheringSpeed;

    public float gatheringDelay;

    public float moveSpeed;

    // 최종속도
    //public float baseSpeed;
    public PlayerStatus()
    {
        currentHealth = 0;
        maxHP = 0;
        playerDamage = 0;
        itemDetectionRange = 0;
        gatheringSpeed = 0;
        gatheringDelay = 0;
        moveSpeed = 0;
    }
    public PlayerStatus(float nowHP,float maxHp,float playerDamage,float detectionRange, float gatherSpeed,float gatherDelay,float moveSpeed)
    {
        currentHealth=nowHP;
        maxHP=maxHp;
        this.playerDamage = playerDamage;
        itemDetectionRange=detectionRange;
        gatheringSpeed = gatherSpeed;
        gatheringDelay = gatherDelay;
        this.moveSpeed = moveSpeed;
    }
    
    public PlayerStatus Clone()
    {
        PlayerStatus clone = new PlayerStatus();

        clone.currentHealth = currentHealth;

        clone.maxHP = maxHP;

        clone.playerDamage = playerDamage;

        clone.itemDetectionRange = itemDetectionRange;

        clone.gatheringSpeed = gatheringSpeed;

        clone.gatheringDelay = gatheringDelay;

        clone.moveSpeed = moveSpeed;

        return clone;
    }


    public static PlayerStatus operator +(PlayerStatus a, PlayerStatus b)
    {
        if (a == null || b == null) 
        { 
            return null; 
        }
        return new PlayerStatus
        {
            currentHealth = a.currentHealth + b.currentHealth,
            maxHP = a.maxHP + b.maxHP,
            playerDamage = a.playerDamage + b.playerDamage,
            itemDetectionRange = a.itemDetectionRange + b.itemDetectionRange,
            gatheringSpeed = a.gatheringSpeed + b.gatheringSpeed,
            moveSpeed = a.moveSpeed + b.moveSpeed,
            gatheringDelay = a.gatheringDelay + b.gatheringDelay
        };
    }
    public static PlayerStatus operator -(PlayerStatus a, PlayerStatus b)
    {
        if (a == null || b == null)
        {
            return null;
        }

        return new PlayerStatus
        {
            currentHealth = a.currentHealth - b.currentHealth,
            maxHP = a.maxHP - b.maxHP,
            playerDamage = a.playerDamage - b.playerDamage,
            itemDetectionRange = a.itemDetectionRange - b.itemDetectionRange,
            gatheringSpeed = a.gatheringSpeed - b.gatheringSpeed,
            moveSpeed = a.moveSpeed - b.moveSpeed,
            gatheringDelay = a.gatheringDelay - b.gatheringDelay
        };
    }

}