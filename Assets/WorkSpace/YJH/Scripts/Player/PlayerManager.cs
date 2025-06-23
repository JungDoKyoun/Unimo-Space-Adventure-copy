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
            Debug.Log("디버그용 스탯 초기화 적용");
        }
        //OnHealthChanged += DebugHealth;
        //selfManager = this;
        //ResetPlayer();
        Debug.Log("어웨이크 실행 됨");
    }
    public void DebugHealth(float health)
    {
        Debug.Log("현재 체력" + health);
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
        Debug.Log("건설 효과 적용 여부 초기화 실행");
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
        //Debug.Log("씬변경 감지함"+debnum);
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

        Debug.Log("활성화 함수 호출됨" + debnum);
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
        Debug.Log("현재 체력"+status.currentHealth+"\n원본 : "+ originStatus.Clone().currentHealth);

        Debug.Log("최대 체력" + status.maxHealth + "\n원본 : " + originStatus.Clone().maxHealth);

        Debug.Log("채집 간격" + status.gatheringDelay + "\n원본 : " + originStatus.Clone().gatheringDelay);

        Debug.Log("채집 파워" + status.gatheringSpeed + "\n원본 : " + originStatus.Clone().gatheringSpeed);

        Debug.Log("데미지" + status.playerDamage + "\n원본 : " + originStatus.Clone().playerDamage);
        Debug.Log("속도" + status.moveSpeed + "\n원본 : " + originStatus.Clone().moveSpeed);
    }
    private void ResetPlayer()
    {
        //Debug.Log("플레이어 리셋");
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
        //스탯 초기화 필요할 까?
        
        if (gatheringCoroutine != null)
        {
            StopCoroutine(gatheringCoroutine);
        }
        //Debug.Log("플레이어 리셋후 문구:");
        //ShowStatusDebug(PlayerStatus);
    }
    public static void ResetStatus()
    {
        if (ConstructManager.IsBuildEffectAplly == false)//건설 매니저 없이 시작할때
        {
            PlayerStatus = originStatus.Clone();
            //ActiveRelic();
            Debug.Log("플레이어 건설 효과 미적용");

        }
        else
        {

            //라운드 종료시 체력
            //PlayerStatus temp = new PlayerStatus(); //건설효과 + 플레이어 기본 스테이터스
            PlayerStatus = ConstructManager.playerStatus;//이게 건설 매니저의 setfinalstatusto player랑 다를게 없다
            Debug.Log("플레이어 건설 효과 적용");

            //ActiveRelic();
            PlayerStatus temp = PlayerStatus.Clone();
            temp.currentHealth -= gainDemage;
            PlayerStatus = temp;

            //PlayerStatus = temp;
            Debug.Log("플레이어 스탯 초기화 후 문구:");
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
    //                    Debug.Log("속도 유물 적용");
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

                        //Debug.Log("속도 유물 적용");

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

                        //Debug.Log("속도 유물 적용");

                        break;

                    default:

                        //Debug.Log("no exist relic type");

                        break;
                }
            }
        Debug.Log("유물 활성화 종료");
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
            //Debug.Log("데미지 받음?trigger");
        }
    }
}