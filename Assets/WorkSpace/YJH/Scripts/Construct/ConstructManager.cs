using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Extensions;

public class ConstructManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints= new List<Transform>();
    [SerializeField] List<ConstructBase> constructList = new List<ConstructBase>();
    [SerializeField] List<UtilityBuildBase> utilityConstructList = new List<UtilityBuildBase>();

    [Header("UI")]
    [SerializeField] GameObject buildingInfoPanel;
    [SerializeField] Image buildingImage;
    [SerializeField] TMP_Text buildingTitleText;
    [SerializeField] TMP_Text buildingInfoText;
    [SerializeField] TMP_Text buildingRequireText;
    [SerializeField] TMP_Text buildingCostText;
    
    //[SerializeField] List<Button> buildButtons = new List<Button>();
    //[SerializeField] GameObject BuildPanel;

    [SerializeField] TMP_Text constructCostText;

    
    [SerializeField] Button buildInfoBuildButton;

    public List<ConstructBase> ConstructList { get { return constructList;  } private set { constructList = value; } }
    public static ConstructManager Instance { get; private set; }

    public delegate void onConstructCostChange();
    public event onConstructCostChange OnConstructCostChange;


    private Dictionary<string, int> ownBuildCostDic = new Dictionary<string, int>();
    private PlayerManager playerManager;
    public PlayerStatus playerStatus = new PlayerStatus();
    [SerializeField] PlayerStatus originPlayerStatus = new PlayerStatus();
    public PlayerStatus OriginPlayerStatus { get { return originPlayerStatus; } }
    public Dictionary<string, int> OwnBuildCostDic { get { return  ownBuildCostDic; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneChanged;
        OnConstructCostChange += SetConstructCostText;
        SetOwnCost();
            
        ToDictionary();
    }

    public void ToDictionary()
    {
        foreach(var temp in constructList)
        {
            temp.ToDictionary();
        }
    }
    public void Init()//현재는 안씀?
    {
        foreach (var temp in constructList)
        {
            temp.Init();
        }
    }
    public void SetConstructCostText()
    {
        Debug.Log("costupdate");
        string tempText = "";
        foreach (var buildCost in ownBuildCostDic)
        {
            tempText += buildCost.Key + ":" + buildCost.Value + " ";
            
        }

        constructCostText.text = tempText;
    }
    public void TryConstruct(ConstructBase building)
    {
        if(building == null)
        {
            return;
        }
        else if(building.TryConstruct()==false)
        {
            return;
        }
        else
        {
            Debug.Log("buildcom");
            building.ConstructEnd();
            spawnPoints[building.spawnIndex].GetComponent<Image>().sprite = building.buildingImage;
            buildInfoBuildButton.interactable = false;
            int costNum;
            FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(building.BuildCostDic.TryGetValue("MetaCurrency",out costNum) ? costNum : 0 );
            
            
            //블루 프린트 함수 추가하기

            //건설이 완료되었다는 뜻이니까 
            //건설 반영을 해야 함
            //어떻게 해야 할까?
            //건설 베이스에 스폰 인덱스가 있으니까 이 스폰 인덱스를 이용해서 스폰리스트에 접근해서 스폰 리스트 쪽에 반영
            //반영 하는건 뒤 패널을 편집하는 방식으로 이미지를 이용해서 덮어 씌우기? 동일한 이미지를 여러개 다른 버전으로 만들면 될 거 같다
        }
    }

    public List<IStatModifier> ReturnStatEffectList()//게임매니저에서 호출 받아서 자기가 적용시킬 스테이터스에 사용하기, 현재는 사용 X
    {
        List<IStatModifier> tempList= new List<IStatModifier>();
        foreach(var building in constructList)
        {
            foreach(var effect in building.buildEffects)
            {
                if (building.isBuildConstructed == true)
                {
                    tempList.Add(effect);
                }
                
            }
        }
        return tempList;
    }
    public void BuildButtonPressed(string buildID)
    {
        foreach (var temp in constructList)
        {
            if (temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                
            }
        }
    }

    public void ActivePanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void DeactivePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void TempGameStart()
    {
        SceneManager.LoadScene("TestScene");
    }

    //public void ShowBuildPanel()
    //{
    //    BuildPanel.SetActive(true); 
    //}
    //public void DeActiveBuildPanel()
    //{
    //    BuildPanel.SetActive(false);
    //}
    public void ShowBuildInfoPanel(ConstructBase buildingInfo)
    {
        var requireText= "";
        buildingInfoPanel.SetActive(true);
        buildingTitleText.text = buildingInfo.buildName;
        //buildingInfoText.text=
        foreach (var temp in buildingInfo.buildRequires)
        {
            requireText += " "+temp;
        }
        string costText="";
        buildingRequireText.text = requireText;
        foreach (var temp in buildingInfo.BuildCostDic)
        {
            costText += "\""+temp.Key+"\""+":"+temp.Value.ToString()+"";
            
        }
        buildingCostText.text = costText;
        buildingInfoText.text=buildingInfo.buildingDescription;
        buildingImage.sprite=buildingInfo.buildIcon;
        DecideCanBuild( buildingInfo);
        buildInfoBuildButton.onClick.AddListener(() => TryConstruct(buildingInfo));




    }
    public void DeActiveBuildInfoPanel()
    {
        buildingInfoPanel.SetActive(false);
    }
    public void DecideCanBuild(ConstructBase buildingInfo)
    {
        if (buildingInfo.TryConstruct() == false)
        {
            buildInfoBuildButton.interactable = false;
        }
        else
        {
            buildInfoBuildButton.interactable = true;
        }
    }

    public bool TryGetPlayer()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager == null)
        {
            return false;
        }
        else
        {

            return true;
        }
    }
    public void SetPlayer()
    {
        //Debug.Log("player!");
        ActiveBuildEffect();
        //SetFinalStatusToPlayer();
    }

    //public void AddStatEffect(BuildEffect buildEffect)//만들어 넣었지만 쓰진 않을듯? 
    //{
    //    buildEffects.Add(buildEffect);
    //}
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
                            maxHPSum += buildeffect.ReturnFinalStat(originPlayerStatus.maxHP);

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
          

        playerStatus = originPlayerStatus.Clone();// 이거 그냥 더할 양만큼 준비하는게 나을지도? 갈아끼는 식 말고
        playerStatus.moveSpeed += speedSum;
        playerStatus.maxHP += maxHPSum;
        playerStatus.gatheringSpeed += gatherSpeedSum;
        playerStatus.gatheringDelay += gatherDelaySum;
        playerStatus.playerDamage += damageSum;
        playerStatus.itemDetectionRange += gatherRangeSum;





    }
    public void ModifieUtillity(IUtilityBuildEffect buildEffect)
    {
        buildEffect.IUtilityBuildEffect();
    }
    public void ModifieSkill()//별도 인터페이스?
    {

    }

    public void ActiveBuildEffect()
    {
        foreach (var building in constructList)
        {
            if (building.isBuildConstructed == true)
            {
                foreach (var buildeffect in building.buildEffects)
                {
                    ModifieStat(buildeffect);



                }
            }
        }
       
    }

    public void SetOwnCost()
    {
        if (FirebaseDataBaseMgr.Instance == null)
        {
            Debug.Log("firenull!");
        }
        else
        {
            FirebaseDataBaseMgr.Instance.StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(0));
        }
        ownBuildCostDic.Add("Blueprint", FirebaseDataBaseMgr.Blueprint);
        ownBuildCostDic.Add("MetaCurrency", FirebaseDataBaseMgr.MetaCurrency);
        OnConstructCostChange.Invoke();
        
    }
    public void SetFinalStatusToPlayer()
    {
        playerManager.SetPlayerStatus(playerStatus);


    }
    
    private void OnSceneChanged(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "TestScene")//씬이름으로 변경
        {
            if (TryGetPlayer()==true)//플레이어가 있으면
            {
                //Debug.Log("scenecallback");
                //SetPlayer();
            }
            else
            {
                return;
            }
        }
    }

}
