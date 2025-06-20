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
        ////��ȹ �ǵ��� ���� �� �ڵ�� ������ �ʿ��� �� ������������ ���� ü���� �ȵ��ƿ��µ�?
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

        //��ȹ �ǵ��� ���� �� �ڵ�� ������ �ʿ��� �� ������������ ���� ü���� �ȵ��ƿ��µ�?
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
        Debug.Log("���� ü��"+status.currentHealth+"\n���� : "+ originStatus.Clone().currentHealth);

        Debug.Log("�ִ� ü��" + status.maxHealth + "\n���� : " + originStatus.Clone().maxHealth);

        Debug.Log("ä�� ����" + status.gatheringDelay + "\n���� : " + originStatus.Clone().gatheringDelay);

        Debug.Log("ä�� �Ŀ�" + status.gatheringSpeed + "\n���� : " + originStatus.Clone().gatheringSpeed);

        Debug.Log("������" + status.playerDamage + "\n���� : " + originStatus.Clone().playerDamage);
    }
    private void ResetPlayer()
    {
        Debug.Log("�÷��̾� ����");
        playerOwnEnergy = 0;

        isGatheringCoroutineWork = false;

        isSkillRejectActive = false;

        isItemNear = false;

        isGathering = false;

        //playerSpellType.SetState(false);

        //playerSpellType = null;

        if (ConstructManager.IsBuildEffectAplly == false)//�Ǽ� �Ŵ��� ���� �����Ҷ�
        {
            PlayerStatus=originStatus.Clone();

            ShowStatusDebug(PlayerStatus);
            Debug.Log("�Ǽ� ȿ�� ���� ����");
        }
        else
        {
            Debug.Log("�Ǽ� ȿ�� ���� ����");
            //PlayerStatus= originStatus.Clone();
            PlayerStatus forSum= originStatus.Clone();
            ShowStatusDebug(forSum);
            PlayerStatus constructStatus = ConstructManager.playerStatus; //���ϱ⸸ �ϸ� �ɰŰ�
            PlayerStatus temp =constructStatus+forSum; // �̰� �⺻ �����ε� ���� ü���� 
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