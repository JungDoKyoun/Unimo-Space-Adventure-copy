using UnityEngine;
using Photon.Pun;
public class Dash : ISpellType,IStackSpell
{
    private StackSpellScriptableObject skillInfo;

    // 현재 충전되어 있는 충전량
    //[SerializeField] int nowStack;

    // 최대 충전량
    //[SerializeField] int maxStack = 2;

    // 충전에 필요한 시간
    //[SerializeField] float chargeTime = 3f;

    [SerializeField] float chargeTimer;

    // 충전되는 양
    //[SerializeField] int chargeStack = 1;

    // 사용하는 충전량
    //[SerializeField] int useStack = 1;

    // 대쉬 속도
    //[SerializeField] float pushPower = 10f;

    // 대쉬 하는 시간
    //[SerializeField] float dashTime = 1f;

    [SerializeField] float dashTimer;

    // 사용하는 플레이어
    private PlayerManager playerManager;

    private bool isDash = false;

    //public delegate void onSkillUseDenied();
    //
    //public event onSkillUseDenied OnSkillUseDenied;
    private Vector3 initPosition=new Vector3();
    private float dashDistance=5f;
    public PlayerManager PlayerManager { get { return playerManager; } set {playerManager=value ; } }

    public int NowStack { get { return skillInfo.nowStack; } set { skillInfo.nowStack = value; } }

    public int MaxStack { get { return skillInfo.maxStack; } }

    public float ChargeTime { get { return skillInfo.chargeTime; } }

    public int UseStack { get { return skillInfo.useStack; }set { skillInfo.useStack = value; } }

    public float Timer { get { return chargeTimer; }  }

    [PunRPC]
    public void UseSpell()
    {

        if (skillInfo.nowStack >= skillInfo.useStack && isDash == false)
        {
            playerManager.PlayerRigBody.constraints = RigidbodyConstraints.FreezeRotation|RigidbodyConstraints.FreezePositionY;
            initPosition= playerManager.transform.position;
            //Debug.Log("dashed");

            skillInfo.nowStack -= skillInfo.useStack;

            playerManager.canMove = false;

            isDash = true;

            if (playerManager.PlayerMoveDirection.magnitude < float.Epsilon)
            {
                playerManager.PlayerRigBody.AddForce(playerManager.transform.forward * skillInfo.skillPower* PlayerManager.PlayerStatus.moveSpeed, ForceMode.Impulse);
            }

            else
            {
                playerManager.gameObject.transform.LookAt(playerManager.PlayerMoveDirection+ playerManager.gameObject.transform.position);
                playerManager.PlayerRigBody.AddForce(playerManager.PlayerMoveDirection.normalized * skillInfo.skillPower * PlayerManager.PlayerStatus.moveSpeed, ForceMode.Impulse);
            }
        }

        else
        {
            playerManager.ActiveSkillReject();
        }
    }

    public void UpdateTime()
    {
        if (skillInfo == null)
        {
            Debug.Log("스킬정보 없음");
        }
        if (skillInfo.nowStack < skillInfo.maxStack)
        {
            chargeTimer += Time.deltaTime;
        }
        if (isDash == true)
        {
            if(playerManager!=null)
            {
                if ((playerManager.transform.position - initPosition).magnitude >= dashDistance)
                {
                    StopSpell();
                }
            }
        }
        //else if(skillInfo.nowStack == skillInfo.maxStack)
        //{
        //    chargeTimer= skillInfo.chargeTime-float.Epsilon;
        //}
        //chargeTimer += Time.deltaTime;
        if (playerManager.canMove == false)
        {
            dashTimer += Time.deltaTime;
        }

        if (chargeTimer > skillInfo.chargeTime)
        {
            if (skillInfo.nowStack < skillInfo.maxStack)
            {
                //Debug.Log("stack charged");

                skillInfo.nowStack += skillInfo.chargeStack;
                chargeTimer = 0;
            }
            else
            {
                chargeTimer = skillInfo.chargeTime;
            }

            

        }
        
        if (dashTimer >= skillInfo.skillTime)
        {
            StopSpell();
        }
    }

    public bool ReturnState()
    {
        return isDash;
    }
    
    public void InitSpell()
    {
        skillInfo = Resources.Load<StackSpellScriptableObject>("PlayerSkillSO/Dash");
        if(skillInfo == null)
        {
            //Debug.Log("스킬 정보 받아오기 실패");
        }
        else
        {
           // Debug.Log("스킬 정보 존재함");
        }
            chargeTimer = 0;
    }

    public void SetPlayer(PlayerManager player)
    {
        playerManager=player;
    }

    public void SetState(bool state)
    {
        isDash = state;
    }

    public void StopSpell()
    {
        playerManager.canMove = true;

        isDash = false;

        playerManager.PlayerRigBody.constraints = RigidbodyConstraints.FreezeAll;

        dashTimer = 0;
    }
}