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

    private int debnum = 0;

    private static bool isFirstRelicActive = true;

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
            }

            else
            {
                playerStatus = value;

                OnHealthChanged?.Invoke(playerStatus.currentHealth);
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

            playerSpellType?.SetPlayer(selfManager);
        }

        if (ConstructManager.Instance== null)
        {
            PlayerStatus= originStatus.Clone();
        }
    }

    public void DebugHealth(float health)
    {
        Debug.Log("���� ü��" + health);
    }

    private void OnDestroy()
    {
        ISingleton<PlayerManager>.Release(this);

        playerOwnEnergy = 0;

        isGatheringCoroutineWork = false;

        isSkillRejectActive = false;

        isItemNear = false;

        isGathering = false;

        if (selfManager == this)
        {
            selfManager = null;
        } 
    }

    private void OnDisable()
    {
        if (ConstructManager.Instance != null)
        {
            ConstructManager.Instance.ResetApplyBuildEffect();
        }
    }

    private void OnEnable()
    {
        if (playerSpellType == null)
        {
            SetSpellType(new Dash());

            playerSpellType?.SetPlayer(selfManager);

            playerSpellType.InitSpell();
        }

        else
        {
            playerSpellType?.SetPlayer(selfManager);

            playerSpellType.InitSpell();
        }

        ResetPlayer();

        ActionStart();

        MoveStart();

        debnum++;
    }

    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
    }

    public static void ShowStatusDebug(PlayerStatus status)
    {
        Debug.Log("���� ü��"+status.currentHealth+"\n���� : "+ originStatus.Clone().currentHealth);

        Debug.Log("�ִ� ü��" + status.maxHealth + "\n���� : " + originStatus.Clone().maxHealth);

        Debug.Log("ä�� ����" + status.gatheringDelay + "\n���� : " + originStatus.Clone().gatheringDelay);

        Debug.Log("ä�� �Ŀ�" + status.gatheringSpeed + "\n���� : " + originStatus.Clone().gatheringSpeed);

        Debug.Log("������" + status.playerDamage + "\n���� : " + originStatus.Clone().playerDamage);

        Debug.Log("�ӵ�" + status.moveSpeed + "\n���� : " + originStatus.Clone().moveSpeed);
    }

    private void ResetPlayer()
    {
        playerOwnEnergy = 0;

        isGatheringCoroutineWork = false;

        isSkillRejectActive = false;

        isItemNear = false;

        isGathering = false;

        canMove = true;

        IsOnHit = false;

        targetObject = null;

        if (playerSpellType != null)
        {
            playerSpellType.SetState(false);
        }
        
        if (gatheringCoroutine != null)
        {
            StopCoroutine(gatheringCoroutine);
        }
    }

    public static void ResetStatus()
    {
        if (ConstructManager.Instance == null)
        {
            PlayerStatus = originStatus.Clone();
        }

        else
        {
            PlayerStatus = ConstructManager.playerStatus;

            PlayerStatus temp = PlayerStatus.Clone();

            temp.currentHealth -= gainDamage;

            PlayerStatus = temp;
        }
    }

    public static void DeactiveRelic(RelicData relicData)
    {
        PlayerStatus temp = PlayerStatus.Clone();

        foreach (var relicEffect in relicData.Effects)
        {
            switch (relicEffect.Type)
            {
                case RelicEffectType.AttackPower:

                    temp = PlayerStatus.Clone();

                    temp.playerDamage -= relicEffect.Value;

                    PlayerStatus = temp;

                    break;

                case RelicEffectType.MaxHealth:

                    temp = PlayerStatus.Clone();

                    temp.currentHealth -= relicEffect.Value;

                    temp.maxHealth += relicEffect.Value;

                    PlayerStatus = temp;

                    break;

                case RelicEffectType.MovementSpeed:

                    temp = PlayerStatus.Clone();

                    temp.moveSpeed -= relicEffect.Value;

                    PlayerStatus = temp;

                    break;

                default:

                    Debug.Log("no exist relic type");

                    break;
            }
        }
    }

    public static void ActiveRelic(RelicData relicData)
    {
        PlayerStatus temp = PlayerStatus.Clone();

        foreach (var relicEffect in relicData.Effects)
        {
            switch (relicEffect.Type)
            {
                case RelicEffectType.AttackPower:

                    temp = PlayerStatus.Clone();

                    temp.playerDamage += relicEffect.Value;

                    PlayerStatus = temp;

                    break;

                case RelicEffectType.MaxHealth:

                    temp = PlayerStatus.Clone();

                    temp.currentHealth += relicEffect.Value;

                    temp.maxHealth += relicEffect.Value;

                    PlayerStatus = temp;

                    break;

                case RelicEffectType.MovementSpeed:

                    temp = PlayerStatus.Clone();

                    temp.moveSpeed += relicEffect.Value;

                    PlayerStatus = temp;

                    break;

                default:

                    break;
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