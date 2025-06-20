using JDG;

using Photon.Pun;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity;

using ZL.Unity.Phys;

using ZL.Unity.Unimo;

public partial class PlayerManager : ISingleton<PlayerManager>
{
    public static PlayerManager Instance
    {
        get => ISingleton<PlayerManager>.Instance;
    }

    private static PlayerStatus originStatus = new PlayerStatus(10, 10, 5, 5, 4, 0.5f, 4);

    private static PlayerStatus playerStatus = new PlayerStatus();
    
    public static PlayerStatus PlayerStatus
    {  
        get 
        { 
            return playerStatus;
        } 
        
        set
        {
            if (playerStatus.currentHealth != value.currentHealth || playerStatus.maxHealth != value.maxHealth)
            {
                if (value.currentHealth < 0)
                {
                    value.currentHealth = 0;
                }

                playerStatus = value;

                OnHealthChanged?.Invoke(playerStatus.currentHealth);
                
                //Debug.Log("setby0");
            }

            else
            {
                playerStatus = value;

                //Debug.Log("set");
            }
        } 
    }

    public static PlayerStatus OriginStatus { get { return originStatus; } }

    private void Awake()
    {
        ISingleton<PlayerManager>.TrySetInstance(this);

        if (selfManager != null)
        {
            return;
        }

        else
        {
            selfManager = this;

            //Debug.Log(playerSpellType);

            playerSpellType?.SetPlayer(selfManager);
        }

        //selfManager = this;
    }

    private void OnDestroy()
    {
        ISingleton<PlayerManager>.Release(this);

        if (selfManager == this)
        {
            selfManager = null;
        }
        OnTargetObjectSet -= GatheringItem;
    }

    private void Start()
    {
        //if (playerSpellType == null)
        //{
        //    SetSpellType(new Dash());
        //
        //    playerSpellType.InitSpell();
        //}
        //
        //if (GameStateManager.IsClear == false)
        //{
        //    ResetPlayer();
        //}
        //
        //ActionStart();
        //
        //MoveStart();
        //
        ////ConstructManager.SetFinalStatusToPlayer();
        //
        ////PlayerInventoryManager.AddRelic(tempRelic);
        //
        //ActiveRelic();
        //
        //ShowStatusDebug();
        //
        ////기획 의도를 보니 이 코드는 조정이 필요함 한 스테이지에서 까인 체력은 안돌아오는듯?
        ////currentHealth = maxHP;
        //
        ////SetPlayerStatus(playerStatus);
    }
    private void OnEnable()
    {
        if (playerSpellType == null)
        {
            SetSpellType(new Dash());

            playerSpellType.InitSpell();
        }

        
        ResetPlayer();
        

        ActionStart();

        MoveStart();

        //ConstructManager.SetFinalStatusToPlayer();

        //PlayerInventoryManager.AddRelic(tempRelic);

        ActiveRelic();

        ShowStatusDebug();

        //기획 의도를 보니 이 코드는 조정이 필요함 한 스테이지에서 까인 체력은 안돌아오는듯?
        //currentHealth = maxHP;

        //SetPlayerStatus(playerStatus);
    }
    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }

    public void ShowStatusDebug()
    {
        //Debug.Log(playerStatus.currentHealth);

        //Debug.Log(playerStatus.maxHealth);

        //Debug.Log(playerStatus.gatheringDelay);

        //Debug.Log(playerStatus.gatheringSpeed);

        //Debug.Log(playerStatus.playerDamage);

        //Debug.Log(originStatus.Clone().currentHealth);

        //Debug.Log(originStatus.Clone().maxHealth);

        //Debug.Log(originStatus.Clone().gatheringDelay);

        //Debug.Log(originStatus.Clone().gatheringSpeed);

        //Debug.Log(originStatus.Clone().playerDamage);
    }
    public void ShowStatusDebug(PlayerStatus status)
    {
        Debug.Log("현재 체력"+status.currentHealth+"\n원본 : "+ originStatus.Clone().currentHealth);

        Debug.Log("최대 체력" + status.maxHealth + "\n원본 : " + originStatus.Clone().maxHealth);

        Debug.Log("채집 간격" + status.gatheringDelay + "\n원본 : " + originStatus.Clone().gatheringDelay);

        Debug.Log("채집 파워" + status.gatheringSpeed + "\n원본 : " + originStatus.Clone().gatheringSpeed);

        Debug.Log("데미지" + status.playerDamage + "\n원본 : " + originStatus.Clone().playerDamage);
    }
    private void ResetPlayer()
    {
        Debug.Log("플레이어 리셋");
        playerOwnEnergy = 0;

        isGatheringCoroutineWork = false;

        isSkillRejectActive = false;

        isItemNear = false;

        isGathering = false;

        //playerSpellType.SetState(false);

        //playerSpellType = null;

        if (ConstructManager.IsBuildEffectAplly == false)//건설 매니저 없이 시작할때
        {
            PlayerStatus=originStatus.Clone();

            ShowStatusDebug(PlayerStatus);
            Debug.Log("건설 효과 적용 없음");
        }
        else
        {
            Debug.Log("건설 효과 적용 있음");
            //PlayerStatus= originStatus.Clone();
            PlayerStatus forSum= originStatus.Clone();
            ShowStatusDebug(forSum);
            PlayerStatus constructStatus = ConstructManager.playerStatus; //더하기만 하면 될거고
            PlayerStatus temp =constructStatus+forSum; // 이게 기본 스탯인데 현재 체력은 
            ShowStatusDebug(temp);
            temp.currentHealth= PlayerStatus.currentHealth;
            PlayerStatus = temp;
        }
    }
    public static void ActiveRelic()
    {
        //Debug.Log("try use relic");

        //Debug.Log(PlayerInventoryManager.RelicDatas.Count);

        PlayerStatus temp = PlayerStatus.Clone();

        foreach (var relic in PlayerInventoryManager.RelicDatas)
        {
            //Debug.Log("relic data exist");
            foreach (var relicEffect in relic.Effects)
            {
                //Debug.Log("relic effect exist");
                switch (relicEffect.Type)
                {
                    case ZL.Unity.Unimo.RelicEffectType.AttackPower:
                        
                        temp=PlayerStatus.Clone();

                        temp.playerDamage += relicEffect.Value;

                        PlayerStatus = temp;

                        //Debug.Log(PlayerStatus.playerDamage);

                        break;

                    case ZL.Unity.Unimo.RelicEffectType.MaxHealth:

                        temp = PlayerStatus.Clone();

                        temp.maxHealth += relicEffect.Value;

                        temp.currentHealth += relicEffect.Value;

                        PlayerStatus = temp;
                        
                        // Debug.Log(PlayerStatus.maxHealth);

                        break;

                    case ZL.Unity.Unimo.RelicEffectType.MovementSpeed:

                        temp = PlayerStatus.Clone();

                        temp.moveSpeed += relicEffect.Value;

                        PlayerStatus= temp;

                        //Debug.Log(PlayerStatus.moveSpeed);

                        break;

                    default:
                        
                        //Debug.Log("no exist relic type");

                        break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Item"))
        {
            var item = other.GetComponent<Item>();

            item.GetItem(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (PhotonNetwork.IsConnected == false)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Gathering"))
            {
                isItemNear = true;
            }

            else
            {
                isItemNear = false;
            }
        }

        else
        {
            if (photonView.IsMine == true)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Gathering"))
                {
                    isItemNear = true;
                }

                else
                {
                    isItemNear = false;
                }
            }

            else
            {
                return;
            }
        }

        if (isOnHit == true)
        {
            return;
        }

        if (enemyLayerMask.Contains(other.gameObject.layer) == false)
        {
            return;
        }

        if (other.gameObject.TryGetComponent<IDamager>(out var damager) == true)
        {
            var contact = mainCollider.ClosestPoint(other);

            damager.GiveDamage(this, contact);
        }
    }
}