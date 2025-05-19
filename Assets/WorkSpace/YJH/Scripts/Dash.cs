using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash :ISpellType,IStackSpell
{

    private StackSpellScriptableObject skillInfo;
    //[SerializeField] int nowStack;// 현재 충전되어 있는 충전량
    //[SerializeField] int maxStack=2;// 최대 충전량
    //[SerializeField] float chargeTime=3f;// 충전에 필요한 시간
    [SerializeField] float chargeTimer;// 
    //[SerializeField] int chargeStack=1;// 충전되는 양

    //[SerializeField] int useStack=1;// 사용하는 충전량
    //[SerializeField] float pushPower = 10f;// 대쉬 속도
    //[SerializeField] float dashTime = 1f;// 대쉬 하는 시간
    [SerializeField] float dashTimer; // 
    
    private PlayerManager playerManager;// 사용하는 플레이어
    private bool isDash=false;

    public delegate void onSkillUseDenied();
    public event onSkillUseDenied OnSkillUseDenied;






    public PlayerManager PlayerManager { get { return playerManager; } set {playerManager=value ; } }

    public int NowStack { get { return skillInfo.nowStack; } set { skillInfo.nowStack = value; } }

    public int MaxStack { get { return skillInfo.maxStack; } }

    public float ChargeTime { get { return skillInfo.chargeTime; } }

    public int UseStack { get { return skillInfo.useStack; }set { skillInfo.useStack = value; } }

    public void UseSpell()
    {
        if (skillInfo.nowStack >= skillInfo.useStack && isDash == false)
        {
            playerManager.PlayerRigBody.constraints = RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionY;
            //Debug.Log("dashed");
            skillInfo.nowStack -= skillInfo.useStack;

            playerManager.canMove = false;
            isDash = true;
            if (playerManager.PlayerMoveDirection.magnitude < float.Epsilon)
            {
                playerManager.PlayerRigBody.AddForce(playerManager.transform.forward * skillInfo.skillPower, ForceMode.Impulse);
            }
            else
            {
                playerManager.PlayerRigBody.AddForce(playerManager.PlayerMoveDirection.normalized * skillInfo.skillPower, ForceMode.Impulse);
            }

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
        


        if (chargeTimer >= skillInfo.chargeTime)
        {
            if (skillInfo.nowStack < skillInfo.maxStack)
            {
                Debug.Log("stack charged");
                skillInfo.nowStack += skillInfo.chargeStack;
            }
            chargeTimer = 0;
            
        }
        if (dashTimer >= skillInfo.skillTime)
        {
            playerManager.canMove = true;
            isDash = false;
            playerManager.PlayerRigBody.constraints = RigidbodyConstraints.FreezeAll;
            dashTimer = 0;
        }
    }
    public bool ReturnState()
    {
        return isDash;
    }
    
    public void InitSpell()
    {
        skillInfo = Resources.Load<StackSpellScriptableObject>("PlayerSkillSO/Dash");

    }

    
    public void SetPlayer(PlayerManager player)
    {
        playerManager=player;
    }



   
}
