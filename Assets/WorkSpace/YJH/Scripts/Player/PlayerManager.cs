using Photon.Pun;

using UnityEngine;

using ZL.Unity;

using ZL.Unity.Phys;

using ZL.Unity.Unimo;

public partial class PlayerManager 
{
    private static PlayerStatus originStatus = new PlayerStatus(10, 10, 5, 5, 4, 0.5f, 4);

    private static PlayerStatus playerStatus = originStatus.Clone();
    
    public static PlayerStatus PlayerStatus {  get { return playerStatus; } set { playerStatus = value; } }

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
        ActionStart();

        MoveStart();
        ConstructManager.SetFinalStatusToPlayer();
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
        Debug.Log(playerStatus.currentHealth);
        Debug.Log(playerStatus.maxHP);
        Debug.Log(playerStatus.gatheringDelay);
        Debug.Log(playerStatus.gatheringSpeed );
        Debug.Log(playerStatus.playerDamage);

    }

    public static void ActiveRelic()
    {
        foreach (var relic in PlayerInventoryManager.RelicDatas)
        {
            foreach (var relicEffect in relic.Effects)
            {
                switch (relicEffect.Type)
                {
                    case RelicEffectType.AttackPower:
                        playerStatus.playerDamage += relicEffect.Value;
                        break;
                    case RelicEffectType.MaxHealth:
                        playerStatus.maxHP += relicEffect.Value;
                        break;
                    case RelicEffectType.MovementSpeed:
                        playerStatus.moveSpeed += relicEffect.Value;
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