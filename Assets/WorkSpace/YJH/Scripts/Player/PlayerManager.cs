using JDG;
using Photon.Pun;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Phys;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    private static PlayerStatus originStatus = new PlayerStatus(10, 10, 5, 5, 4, 0.5f, 4);

    private static PlayerStatus playerStatus = new PlayerStatus();
    
    public static PlayerStatus PlayerStatus {  
        get 
        { 
            return playerStatus;
        } 
        
        set
        {
            if (playerStatus.currentHealth != value.currentHealth||playerStatus.maxHealth!=value.maxHealth)
            {
                if (value.currentHealth < 0)
                {
                    value.currentHealth = 0;
                }
                playerStatus=value;
                OnHealthChanged?.Invoke(playerStatus.currentHealth);
                //Debug.Log(value.maxHealth);
                
                Debug.Log("setby0");
            }
            else
            {
                playerStatus = value;
                Debug.Log("set");
            }
        } 
    }

    public static PlayerStatus OriginStatus { get { return originStatus; } }

    private void OnDestroy()
    {
        if (selfManager == this)
        {
            selfManager = null;
        }
    }

    private void Awake()
    {
        if (selfManager != null)
        {
            return;
        }

        else
        {
            selfManager = this;

            //Debug.Log(playerSpellType);

            if (playerSpellType != null)
            {
                playerSpellType.SetPlayer(selfManager);
            }
        }

        //selfManager = this;
    }

    private void Start()
    {
        if (playerSpellType == null)
        {
            SetSpellType(new Dash());

            playerSpellType.InitSpell();
        }
        if (GameStateManager.IsClear == false)
        {
            ResetPlayer();
        }
        ActionStart();

        MoveStart();
        //ConstructManager.SetFinalStatusToPlayer();
        //PlayerInventoryManager.AddRelic(tempRelic);
        ActiveRelic();
        ShowStatusDebug();
        //currentHealth = maxHP;//기획 의도를 보니 이 코드는 조정이 필요함 한 스테이지에서 까인 체력은 안돌아오는듯?
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
        //
        //Debug.Log(playerStatus.maxHealth);
        //
        //Debug.Log(playerStatus.gatheringDelay);
        //
        //Debug.Log(playerStatus.gatheringSpeed);
        //
        //Debug.Log(playerStatus.playerDamage);
        //
        //Debug.Log(originStatus.Clone().currentHealth);
        //
        //Debug.Log(originStatus.Clone().maxHealth);
        //
        //Debug.Log(originStatus.Clone().gatheringDelay);
        //
        //Debug.Log(originStatus.Clone().gatheringSpeed);
        //
        //Debug.Log(originStatus.Clone().playerDamage);
    }

    private void ResetPlayer()
    {
        playerOwnEnergy = 0;

        isGatheringCoroutineWork = false;

        isSkillRejectActive = false;

        isItemNear = false;

        isGathering = false;

        //playerSpellType.SetState(false);

        playerSpellType = null;
        if (ConstructManager.IsBuildEffectAplly == false)
        {
            PlayerStatus=originStatus.Clone();
            ShowStatusDebug();
            Debug.Log("건설 매니저 거치지 않음");
            //Debug.Log(PlayerStatus.currentHealth);
        }
    }
    public static void ActiveRelic()
    {
        //Debug.Log("try use relic");

        //Debug.Log(PlayerInventoryManager.RelicDatas.Count);

        foreach (var relic in PlayerInventoryManager.RelicDatas)
        {
            Debug.Log("relic data exist");
            foreach (var relicEffect in relic.Effects)
            {
                Debug.Log("relic effect exist");
                switch (relicEffect.Type)
                {
                    case ZL.Unity.Unimo.RelicEffectType.AttackPower:
                        PlayerStatus.playerDamage += relicEffect.Value;
                        Debug.Log(PlayerStatus.playerDamage);
                        break;
                    case ZL.Unity.Unimo.RelicEffectType.MaxHealth:
                        PlayerStatus.maxHealth += relicEffect.Value;
                        Debug.Log(PlayerStatus.maxHealth);
                        break;
                    case ZL.Unity.Unimo.RelicEffectType.MovementSpeed:
                        PlayerStatus.moveSpeed += relicEffect.Value;
                        Debug.Log(PlayerStatus.moveSpeed);
                        break;
                    default:
                        Debug.Log("no exist relic type");
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