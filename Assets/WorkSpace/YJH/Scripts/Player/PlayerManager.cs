using JDG;

using Photon.Pun;

using UnityEngine;
using ZL.CS.Singleton;

using ZL.Unity;

using ZL.Unity.Phys;

using ZL.Unity.Unimo;
using UnityEngine.SceneManagement;
public partial class PlayerManager : ISingleton<PlayerManager>
{
    public static PlayerManager Instance
    {
        get => ISingleton<PlayerManager>.Instance;
    }

    private static PlayerStatus originStatus = new PlayerStatus(10, 10, 5, 5, 4, 0.5f, 4);

    private static PlayerStatus playerStatus = new PlayerStatus();
    private int debnum=0;
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
                
                //Debug.Log("setby0");
            }else
            {
                playerStatus = value;
                OnHealthChanged?.Invoke(playerStatus.currentHealth);
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
        //ConstructManager.SetFinalStatusToPlayer();
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (ConstructManager.Instance == null)
        {
            PlayerStatus = originStatus.Clone();
            Debug.Log("����׿� ���� �ʱ�ȭ ����");
        }
        //OnHealthChanged += DebugHealth;
        //selfManager = this;
        //ResetPlayer();
        Debug.Log("�����ũ ���� ��");
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
        //OnTargetObjectSet -= GatheringItem;
        //SceneManager.sceneLoaded -= OnSceneLoaded;  
    }
    private void OnDisable()
    {
        ConstructManager.Instance.ResetApplyBuildEffect();
        Debug.Log("�Ǽ� ȿ�� ���� ���� �ʱ�ȭ ����");
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
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        //if (playerSpellType == null)
        //{
        //    SetSpellType(new Dash());
        //    playerSpellType?.SetPlayer(selfManager);
        //    playerSpellType.InitSpell();
        //}
        //else
        //{
        //    playerSpellType?.SetPlayer(selfManager);
        //    playerSpellType.InitSpell();
        //}
        //
        //
        //ResetPlayer();
        //
        //ActionStart();
        //
        //MoveStart();
        //
        //Debug.Log("������ ������"+debnum);
        //debnum++;

        //ConstructManager.SetFinalStatusToPlayer();

        //PlayerInventoryManager.AddRelic(tempRelic);

        
        
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

        Debug.Log("Ȱ��ȭ �Լ� ȣ���" + debnum);
        debnum++;

    }
    private void Update()
    {
        ActionUpdate();

        MoveUpdate();
        //Debug.Log(isOnHit);
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
        //Debug.Log("�÷��̾� ����");
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
        //���� �ʱ�ȭ �ʿ��� ��?
        
        if (gatheringCoroutine != null)
        {
            StopCoroutine(gatheringCoroutine);
        }
        //Debug.Log("�÷��̾� ������ ����:");
        //ShowStatusDebug(PlayerStatus);
    }
    public static void ResetStatus()
    {
        if (ConstructManager.IsBuildEffectAplly == false)//�Ǽ� �Ŵ��� ���� �����Ҷ�
        {
            PlayerStatus = originStatus.Clone();
            //ActiveRelic();
            Debug.Log("�÷��̾� �Ǽ� ȿ�� ������");

        }
        else
        {

            //���� ����� ü��
            //PlayerStatus temp = new PlayerStatus(); //�Ǽ�ȿ�� + �÷��̾� �⺻ �������ͽ�
            PlayerStatus = ConstructManager.playerStatus;//�̰� �Ǽ� �Ŵ����� setfinalstatusto player�� �ٸ��� ����
            Debug.Log("�÷��̾� �Ǽ� ȿ�� ����");

            //ActiveRelic();
            PlayerStatus temp = PlayerStatus.Clone();
            temp.currentHealth -= gainDemage;
            PlayerStatus = temp;

            //PlayerStatus = temp;
            Debug.Log("�÷��̾� ���� �ʱ�ȭ �� ����:");
            ShowStatusDebug(PlayerStatus);

        }
    }
    
    //public static void ActiveRelic()
    //{
    //    //isFirstRelicActive = false;
    //    //Debug.Log("try use relic");
    //
    //    //Debug.Log(PlayerInventoryManager.RelicDatas.Count);
    //
    //    PlayerStatus temp = PlayerStatus.Clone();
    //
    //    foreach (var relic in PlayerInventoryManager.RelicDatas)
    //    {
    //        //Debug.Log("relic data exist");
    //        foreach (var relicEffect in relic.Effects)
    //        {
    //            //Debug.Log("relic effect exist");
    //            switch (relicEffect.Type)
    //            {
    //                case ZL.Unity.Unimo.RelicEffectType.AttackPower:
    //                    
    //                    temp=PlayerStatus.Clone();
    //
    //                    temp.playerDamage += relicEffect.Value;
    //
    //                    PlayerStatus = temp;
    //
    //                    //Debug.Log(PlayerStatus.playerDamage);
    //
    //                    break;
    //
    //                case ZL.Unity.Unimo.RelicEffectType.MaxHealth:
    //
    //                    temp = PlayerStatus.Clone();
    //                    
    //                    temp.currentHealth += relicEffect.Value;
    //                    
    //                    temp.maxHealth += relicEffect.Value;
    //
    //                    
    //
    //                    PlayerStatus = temp;
    //                    
    //                    // Debug.Log(PlayerStatus.maxHealth);
    //
    //                    break;
    //
    //                case ZL.Unity.Unimo.RelicEffectType.MovementSpeed:
    //
    //                    temp = PlayerStatus.Clone();
    //
    //                    temp.moveSpeed += relicEffect.Value;
    //
    //                    PlayerStatus= temp;
    //
    //                    Debug.Log("�ӵ� ���� ����");
    //
    //                    break;
    //
    //                default:
    //                    
    //                    Debug.Log("no exist relic type");
    //
    //                    break;
    //            }
    //        }
    //    }
    //}
    public static void DeactiveRelic(RelicData relicData)
    {
        //isFirstRelicActive = false;
        //Debug.Log("try use relic");

        //Debug.Log(PlayerInventoryManager.RelicDatas.Count);

        PlayerStatus temp = PlayerStatus.Clone();

        
            //Debug.Log("relic data exist");
            foreach (var relicEffect in relicData.Effects)
            {
                //Debug.Log("relic effect exist");
                switch (relicEffect.Type)
                {
                    case ZL.Unity.Unimo.RelicEffectType.AttackPower:

                        temp = PlayerStatus.Clone();

                        temp.playerDamage -= relicEffect.Value;

                        PlayerStatus = temp;

                        //Debug.Log(PlayerStatus.playerDamage);

                        break;

                    case ZL.Unity.Unimo.RelicEffectType.MaxHealth:

                        temp = PlayerStatus.Clone();

                        temp.currentHealth -= relicEffect.Value;

                        temp.maxHealth += relicEffect.Value;



                        PlayerStatus = temp;

                        // Debug.Log(PlayerStatus.maxHealth);

                        break;

                    case ZL.Unity.Unimo.RelicEffectType.MovementSpeed:

                        temp = PlayerStatus.Clone();

                        temp.moveSpeed -= relicEffect.Value;

                        PlayerStatus = temp;

                        //Debug.Log("�ӵ� ���� ����");

                        break;

                    default:

                        Debug.Log("no exist relic type");

                        break;
                }
            }
        
    }
    public static void ActiveRelic(RelicData relicData)
    {
        //isFirstRelicActive = false;
        Debug.Log("try use relic");

        //Debug.Log(PlayerInventoryManager.RelicDatas.Count);

        PlayerStatus temp = PlayerStatus.Clone();

        
            //Debug.Log("relic data exist");
            foreach (var relicEffect in relicData.Effects)
            {
                //Debug.Log("relic effect exist");
                switch (relicEffect.Type)
                {
                    case ZL.Unity.Unimo.RelicEffectType.AttackPower:

                        temp = PlayerStatus.Clone();

                        temp.playerDamage += relicEffect.Value;

                        PlayerStatus = temp;

                        //Debug.Log(PlayerStatus.playerDamage);

                        break;

                    case ZL.Unity.Unimo.RelicEffectType.MaxHealth:

                        temp = PlayerStatus.Clone();

                        temp.currentHealth += relicEffect.Value;

                        temp.maxHealth += relicEffect.Value;



                        PlayerStatus = temp;

                        // Debug.Log(PlayerStatus.maxHealth);

                        break;

                    case ZL.Unity.Unimo.RelicEffectType.MovementSpeed:

                        temp = PlayerStatus.Clone();

                        temp.moveSpeed += relicEffect.Value;

                        PlayerStatus = temp;

                        //Debug.Log("�ӵ� ���� ����");

                        break;

                    default:

                        //Debug.Log("no exist relic type");

                        break;
                }
            }
        Debug.Log("���� Ȱ��ȭ ����");
        ShowStatusDebug(PlayerStatus);
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
            //Debug.Log("������ ����?trigger");
        }
    }
}