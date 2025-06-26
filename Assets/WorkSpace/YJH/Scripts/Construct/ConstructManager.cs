using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using ZL.Unity.Unimo;
using JDG;
using ZL.CS.Singleton;
using YJH;

public class ConstructManager : MonoBehaviour
{
    //[SerializeField] List<Transform> spawnPoints= new List<Transform>();
    public List<TechBuildBase> techConstructList = new List<TechBuildBase>();
    public List<UtilityBuildBase> utilityConstructList = new List<UtilityBuildBase>();
    public List<CombatBuildBase> combatConstructList = new List<CombatBuildBase>();
    private Dictionary<string,ConstructBase> allBuildingDic = new Dictionary<string,ConstructBase>();
    

    
    //[SerializeField] List<Sprite> buildStateImageList= new List<Sprite>();//나중에 건설 이미지 방식 바뀌면 삭제할 것 
    
    //private float buildStateProgress = 0;
    //[SerializeField] List<Button> buildButtons = new List<Button>();
    //[SerializeField] GameObject BuildPanel;


    [SerializeField] PlayerStatus originPlayerStatus = new PlayerStatus();
    public static PlayerStatus playerStatus = new PlayerStatus();


    //public List<ConstructBase> ConstructList { get { return constructList;  } private set { constructList = value; } }
    public static List<string> buildedList= new List<string>();    
    public static ConstructManager Instance { get; private set; }

    public delegate void onConstructCostChange();
    public event onConstructCostChange OnConstructCostChange;

    private static bool isBuildEffectAplly=false;
    public static bool IsBuildEffectAplly { get { return isBuildEffectAplly; } }
    private static bool isDelinkON = false;
    public bool isGiveStartRellic = false;
    
    private Dictionary<string, int> ownBuildCostDic = new Dictionary<string, int>();
    public Dictionary<string, int> OwnBuildCostDic { get { return ownBuildCostDic; } }
    //private PlayerManager playerManager;

    public Dictionary<string, ConstructBase> AllBuildingDic
    {
        get { return allBuildingDic; }
    }
    public PlayerStatus OriginPlayerStatus { get { return originPlayerStatus; } }
    
    public static ISpellType[] playerSpells = { null,new Dash() };
    
    [SerializeField] GameObject[] attackPrefabs;

    //public RelicData tempRelic;
    
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
            
        OnConstructCostChange += SetConstructCostText;
        
        //DecideProgress();//나중에 이미지 변경 시스템 완벽하게 바꾸면 변경하기
        ToDictionary();
        SetAllDic();
       
        
        SetAllConstructImages();
        //GameStateManager.IsClear = true;// 버그 터짐 이거 말고 다른 방법 써야 할듯
        playerStatus = PlayerManager.OriginStatus.Clone();
        ActiveBuildEffect();
        PlayerManager.PlayerStatus = playerStatus;
        PlayerManager.OnStageFail += YJH.MethodCollection.DelinkHealPlayer;
        PlayerManager.OnStageFail += ResetApplyBuildEffect;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        DontDestroyOnLoad(gameObject);  
    }

    private void Update()
    {
        if (GameStateManager.IsClear == false)
        {
            ResetApplyBuildEffect();
        }
    }
    private void Start()
    {
        StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(0));
        StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardBluePrint(0));
        
        if (GameStateManager.IsClear == true)
        {
            Debug.Log("스테이지 클리어");
            return;
        }
        else
        {
            Debug.Log("스테이지 클리어 실패");
            isBuildEffectAplly = false;
            PlayerManager.gainDemage = 0;
            PlayerManager.ResetStatus();
            //SetFinalStatusToPlayer();
        }
        
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (GameStateManager.IsClear == true)
        {
            //Debug.Log("스테이지 클리어");
            return;
        }
        else
        {
            //Debug.Log("스테이지 클리어 실패");
            PlayerManager.gainDemage = 0;
            isBuildEffectAplly = false;
            PlayerManager.ResetStatus();
        }
    }


    private void OnDestroy()
    {
        //Debug.Log("건설매니저 사라짐");
        OnConstructCostChange -= SetConstructCostText;
    }
    public void ToDictionary()
    {
        foreach(var temp in techConstructList)
        {
            temp.ToDictionary();
        }
        foreach (var temp in utilityConstructList)
        {
            temp.ToDictionary();
        }
        foreach (var temp in combatConstructList)
        {
            temp.ToDictionary();
        }
    }
    
    private void SetAllDic()
    {
        foreach (var temp in techConstructList)
        {
            allBuildingDic.Add(temp.buildID, temp);
        }
        foreach (var temp in utilityConstructList)
        {
            allBuildingDic.Add(temp.buildID, temp);
        }
        foreach (var temp in combatConstructList)
        {
            allBuildingDic.Add(temp.buildID, temp);
        }
    }

    public void SetConstructCostText()
    {
        //Debug.Log("costupdate");
        string tempText = "";
        foreach (var buildCost in ownBuildCostDic)
        {
            tempText += buildCost.Key + ":" + buildCost.Value + " ";

        }

        ConstructUIManager.Instance.constructCostText.text = tempText;
    }

    public void TryConstruct(ConstructBase building)
    {
        if(building == null)
        {
            return;
        }
        else  
        {
            switch (building)
            {
                case TechBuildBase:
                    if (building.TryConstruct(techConstructList)==false)
                    {
                        return;
                    }

                    break;
                case UtilityBuildBase:
                    if(building.TryConstruct(utilityConstructList)==false)
                    {
                        return;
                    }
                    break;
                case CombatBuildBase:
                    if(building.TryConstruct(combatConstructList) == false)
                    {
                        return;
                    }
                    break;
                default:
                    Debug.Log("non build type");
                    return;
                    
            }
            
        }
        
            //Debug.Log("buildcom");
            building.ConstructEnd();
            ConstructUIManager.Instance.TrySetConstructImage(building);
            //spawnPoints[building.spawnIndex].GetComponent<Image>().sprite = building.buildingImage;
            ConstructUIManager.Instance.buildInfoBuildButton.interactable = false;
            int costNum;
            CoroutineRunner.Instance.Run(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(building.BuildCostDic.TryGetValue("MetaCurrency", out costNum) ? -costNum : 0));
            
        
    }
    

    public void SetAllConstructImages()
    {
        foreach(var temp in techConstructList)
        {
            if (temp.isBuildConstructed == true)
            {
                if (ConstructUIManager.Instance.imagePriority[temp.imageIndex-1] < temp.imagePriority)
                {
                    ConstructUIManager.Instance.buildingImages[temp.imageIndex-1].sprite = temp.buildingImage;
                }
            }
        }
        foreach (var temp in utilityConstructList)
        {
            if (temp.isBuildConstructed == true)
            {
                if (ConstructUIManager.Instance.imagePriority[temp.imageIndex - 1] < temp.imagePriority)
                {
                    ConstructUIManager.Instance.buildingImages[temp.imageIndex - 1].sprite = temp.buildingImage;
                }
            }
        }
        foreach (var temp in combatConstructList)
        {
            if (temp.isBuildConstructed == true)
            {
                if (ConstructUIManager.Instance.imagePriority[temp.imageIndex - 1] < temp.imagePriority)
                {
                    ConstructUIManager.Instance.buildingImages[temp.imageIndex - 1].sprite = temp.buildingImage;
                }
            }
        }
    }




    public void DecideProgress()//나중에 에셋 오면 변경 필요 -> 별도의 스크립트와 씬에서 캡쳐를 통해서 변화 반영하는 식으로 
    {
        //Debug.Log("changeimage");
        int buildingNum = 0;
        int buildedBuildingNum = 0;
        foreach (var building in techConstructList)
        {
            buildingNum++;
            if (building.isBuildConstructed == true)
            {
                buildedBuildingNum++;
                if (buildedList.Contains(building.buildID)==false)
                {
                    buildedList.Add(building.buildID);
                }
                
            }
            
            
        }
        foreach (var building in utilityConstructList)
        {
            buildingNum++;
            if (building.isBuildConstructed == true)
            {
                buildedBuildingNum++;
                if (buildedList.Contains(building.buildID) == false)
                {
                    buildedList.Add(building.buildID);
                }

            }
        }
        foreach (var building in combatConstructList)
        {
            buildingNum++;
            if (building.isBuildConstructed == true)
            {
                buildedBuildingNum++;
                if (buildedList.Contains(building.buildID) == false)
                {
                    buildedList.Add(building.buildID);
                }

            }
        }

        //buildStateProgress =(float)buildedBuildingNum/buildingNum;
       // Debug.Log(buildStateProgress);
        //DebugBuildedList();
        //ChangeBuildStateImage();
    }
    public void DebugBuildedList()
    {
        foreach (var building in buildedList)
        {
            //Debug.Log(building);
        }
    }
    
   public void ResetApplyBuildEffect()
    {
        isBuildEffectAplly = false;
    }
    

    
   

    



    //public void ShowBuildPanel()
    //{
    //    BuildPanel.SetActive(true); 
    //}
    //public void DeActiveBuildPanel()
    //{
    //    BuildPanel.SetActive(false);
    //}
    

    public void DecideCanBuild(ConstructBase buildingInfo)
    {
        switch (buildingInfo)
        {
            case TechBuildBase:
                if (buildingInfo.TryConstruct(techConstructList) == false)
                {
                    ConstructUIManager.Instance.buildInfoBuildButton.interactable = false;
                }
                else
                {
                    ConstructUIManager.Instance.buildInfoBuildButton.interactable = true;
                }
                break;
            case UtilityBuildBase:
                if (buildingInfo.TryConstruct(utilityConstructList) == false)
                {
                    ConstructUIManager.Instance.buildInfoBuildButton.interactable = false;
                }
                else
                {
                    ConstructUIManager.Instance.buildInfoBuildButton.interactable = true;
                }
                break;
            case CombatBuildBase:
                if (buildingInfo.TryConstruct(combatConstructList) == false)
                {
                    ConstructUIManager.Instance.buildInfoBuildButton.interactable = false;
                }
                else
                {
                    ConstructUIManager.Instance.buildInfoBuildButton.interactable = true;
                }
                break;
        }
        
    }

    
    public void SetPlayer()// 게임 종료시 스테이터스 초기화 필요
    {
        if (isBuildEffectAplly == false)//static 변수를 통해서 초기화 조절
        {
            Debug.Log("건설매니저가 플레이어 세팅");
            playerStatus = PlayerManager.OriginStatus.Clone();
            ActiveBuildEffect();
            isBuildEffectAplly = true;
        }
        //if (GameStateManager.IsClear==false)//static 변수를 통해서 초기화 조절
        //{
        //    Debug.Log("플레이어 세팅");
        //    playerStatus = PlayerManager.OriginStatus.Clone();
        //    ActiveBuildEffect();
        //    isBuildEffectAplly = true;
        //}
        //SetFinalStatusToPlayer();
    }

   
    public void ModifieStat(BuildEffect buildeffect)
    {
        float speedSum = 0;
        float maxHPSum = 0;
        float gatherSpeedSum = 0;
        float gatherDelaySum = 0;
        float damageSum = 0;
        float gatherRangeSum = 0;


     
                    switch (buildeffect)
                    {
                        case Speed speed:
                            speedSum += buildeffect.ReturnFinalStat(originPlayerStatus.moveSpeed);

                            break;
                        case MaxHp maxHP:
                            maxHPSum += buildeffect.ReturnFinalStat(originPlayerStatus.maxHealth);

                            break;
                        case GatheringSpeed gatheringSpeed:
                            gatherSpeedSum += buildeffect.ReturnFinalStat(originPlayerStatus.gatheringSpeed);
                            break;
                        case GatheringDelay gatheringDelay:
                            gatherDelaySum += buildeffect.ReturnFinalStat(originPlayerStatus.gatheringDelay);
                            break;
                        case Damage damage:
                           // Debug.Log("adddmg");
                            damageSum += buildeffect.ReturnFinalStat(originPlayerStatus.playerDamage);
                            break;
                        case ItemDetectionRange itemDetectionRange:
                            gatherRangeSum += buildeffect.ReturnFinalStat(originPlayerStatus.itemDetectionRange);
                            break;
                        default:

                            break;

                    }
          

        //playerStatus = PlayerManager.OriginStatus.Clone();// 이거 그냥 더할 양만큼 준비하는게 나을지도? 갈아끼는 식 말고-> 클리어 실패시 초기화 필요하니 오리진에서 더하는 방식으로 하자
        playerStatus.moveSpeed += speedSum;
        playerStatus.maxHealth += maxHPSum;
        playerStatus.currentHealth += maxHPSum;
        playerStatus.gatheringSpeed += gatherSpeedSum;
        playerStatus.gatheringDelay += gatherDelaySum;
        playerStatus.playerDamage += damageSum;
        playerStatus.itemDetectionRange += gatherRangeSum;



        //Debug.Log(playerStatus.playerDamage);

    }
    

    public void ActiveBuildEffect()
    {
        foreach (var building in techConstructList)
        {
            if (building.isBuildConstructed == true)
            {
                //Debug.Log(building.buildID);
                ModifieStat(building.buildEffect);
            }
        }
        foreach (var building in utilityConstructList)
        {
            if (building.isBuildConstructed == true)
            {
                building.UtilityBuildEffect.Excute();
            }
        }
        foreach (var building in combatConstructList)
        {
            if (building.isBuildConstructed == true)
            {
                building.SetPlayerPower();
            }
        }


        //SetFinalStatusToPlayer();
    }
    
    public void SetOwnCost()
    {


        if (FirebaseDataBaseMgr.Instance == null)
        {
            Debug.Log("데이터베이스 없음");
            return;
        }
        else
        {


            //Debug.Log(FirebaseDataBaseMgr.Blueprint);
            //Debug.Log(FirebaseDataBaseMgr.MetaCurrency);
            if (ownBuildCostDic.ContainsKey("Blueprint"))
            {
                ownBuildCostDic["Blueprint"] = FirebaseDataBaseMgr.Blueprint;
            }
            else
            {
                ownBuildCostDic.Add("Blueprint", FirebaseDataBaseMgr.Blueprint);
            }
            if (ownBuildCostDic.ContainsKey("MetaCurrency"))
            {
                ownBuildCostDic["MetaCurrency"] = FirebaseDataBaseMgr.MetaCurrency;
            }
            else
            {
                ownBuildCostDic.Add("MetaCurrency", FirebaseDataBaseMgr.MetaCurrency);
            }
            
            
            
            //Debug.Log("파이어 베이스에서 받아옴");
            OnConstructCostChange.Invoke();

        }
        
        
        
    }
    public static void SetFinalStatusToPlayer()//이거 하나 확인
    {
        PlayerManager.PlayerStatus=playerStatus+PlayerManager.OriginStatus.Clone();//후일 초기화 생각하면 대입이 맞을듯
        Debug.Log("건설매니저 효과 적용");

    }
    
    

}
