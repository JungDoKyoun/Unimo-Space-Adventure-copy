using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash :ISpellType,IStackSpell
{
    [SerializeField] int nowStack;
    [SerializeField] int maxStack=2;
    [SerializeField] float chargeTime=3f;
    [SerializeField] float chargeTimer;

    [SerializeField] int useStack=1;
    [SerializeField] float pushPower = 10f;
    [SerializeField] float dashTime = 1f;
    [SerializeField] float dashTimer; 
    private PlayerManager playerManager;
    public PlayerManager PlayerManager { get { return playerManager; } set {playerManager=value ; } }

    public int NowStack { get { return nowStack; } set { nowStack = value; } }

    public int MaxStack { get { return maxStack; } }

    public float ChargeTime { get { return chargeTime; } }

    public int UseStack { get { return useStack; }set { useStack = value; } }

    public void UseSpell()
    {
        if (nowStack >= useStack)
        {
            playerManager.PlayerRigBody.constraints = RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionY;
            Debug.Log("dashed");
            nowStack -= useStack;

            playerManager.canMove = false;
            playerManager.PlayerRigBody.AddForce(playerManager.PlayerMoveDirection.normalized * pushPower, ForceMode.Impulse);

        }
        else
        {
            Debug.Log("not enough charge");
        }
    }
    public void UpdateTime()
    {
        chargeTimer += Time.deltaTime;
        if (playerManager.canMove == false)
        {
            dashTimer += Time.deltaTime;
        }
        
        if (chargeTimer >= chargeTime)
        {
            if (nowStack < maxStack)
            {
                Debug.Log("stack charged");
                nowStack += 1;
            }
            chargeTimer = 0;
            
        }
        if (dashTimer >= dashTime)
        {
            playerManager.canMove = true;
            playerManager.PlayerRigBody.constraints = RigidbodyConstraints.FreezeAll;
            dashTimer = 0;
        }
    }
    
    
    public void InitSpell()
    {
        

    }

    
    public void SetPlayer(PlayerManager player)
    {
        playerManager=player;
    }



   
}
