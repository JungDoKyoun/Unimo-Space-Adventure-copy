using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;

public class ConstructManager : MonoBehaviour
{
    //[SerializeField] List<Transform> spawnPoints= new List<Transform>();
    [SerializeField] List<TechBuildBase> techConstructList = new List<TechBuildBase>();
    [SerializeField] List<UtilityBuildBase> utilityConstructList = new List<UtilityBuildBase>();
    [SerializeField] List<CombatBuildBase> combatConstructList = new List<CombatBuildBase>();
    private Dictionary<string,ConstructBase> allBuildingDic = new Dictionary<string,ConstructBase>();
    [Header("UI")]
    [SerializeField] GameObject buildingInfoPanel;
    [SerializeField] Image buildingImage;
    [SerializeField] TMP_Text buildingTitleText;
    [SerializeField] TMP_Text buildingInfoText;
    [SerializeField] TMP_Text buildingRequireText;
    [SerializeField] TMP_Text buildingCostText;
    [SerializeField] TMP_Text constructCostText;
    [SerializeField] Button buildInfoBuildButton;
    [SerializeField] GameObject basePanel;

    [Header("�Ǽ��Ϸ� ȭ�� ����")]
    [SerializeField] Image buildStateImage;
    [SerializeField] List<Sprite> buildStateImageList= new List<Sprite>();
    private float buildStateProgress = 0;
    //[SerializeField] List<Button> buildButtons = new List<Button>();
    //[SerializeField] GameObject BuildPanel;


    [SerializeField] PlayerStatus originPlayerStatus = new PlayerStatus();
    public PlayerStatus playerStatus = new PlayerStatus();


    //public List<ConstructBase> ConstructList { get { return constructList;  } private set { constructList = value; } }
    public static List<string> buildedList= new List<string>();    
    public static ConstructManager Instance { get; private set; }

    public delegate void onConstructCostChange();
    public event onConstructCostChange OnConstructCostChange;

    private static bool isBuildEffectAplly=false;
    public Dictionary<string, int> ownBuildCostDic = new Dictionary<string, int>();
    //private PlayerManager playerManager;
    
    
    public PlayerStatus OriginPlayerStatus { get { return originPlayerStatus; } }
    public Dictionary<string, int> OwnBuildCostDic { get { return  ownBuildCostDic; } }
    private ISpellType[] playerSpells = { new Dash() };
    [SerializeField] GameObject[] attackPrefabs;
    private void Awake()
    {
        
        Instance = this;
        OnConstructCostChange += SetConstructCostText;
        SetOwnCost();
        DecideProgress();
        ToDictionary();
        SetAllDic();
        //PlayerManager.OnPlayerDead += YJH.MethodCollection.DelinkHealPlayer;
    }
    private void OnDestroy()
    {
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

        constructCostText.text = tempText;
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
            //spawnPoints[building.spawnIndex].GetComponent<Image>().sprite = building.buildingImage;
            buildInfoBuildButton.interactable = false;
            int costNum;
        StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(building.BuildCostDic.TryGetValue("MetaCurrency", out costNum) ? -costNum : 0));
            //SetPlayer();
            DecideProgress();
            
            //��� ����Ʈ �Լ� �߰��ϱ�

            //�Ǽ��� �Ϸ�Ǿ��ٴ� ���̴ϱ� 
            //�Ǽ� �ݿ��� �ؾ� ��
            //��� �ؾ� �ұ�?
            //�Ǽ� ���̽��� ���� �ε����� �����ϱ� �� ���� �ε����� �̿��ؼ� ��������Ʈ�� �����ؼ� ���� ����Ʈ �ʿ� �ݿ�
            //�ݿ� �ϴ°� �� �г��� �����ϴ� ������� �̹����� �̿��ؼ� ���� �����? ������ �̹����� ������ �ٸ� �������� ����� �� �� ����
        
    }
    public void DecideProgress()
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

        buildStateProgress =(float)buildedBuildingNum/buildingNum;
       // Debug.Log(buildStateProgress);
        //DebugBuildedList();
        ChangeBuildStateImage();
    }
    public void DebugBuildedList()
    {
        foreach (var building in buildedList)
        {
            //Debug.Log(building);
        }
    }
    public void ChangeBuildStateImage()
    {
        //Debug.Log(buildStateImageList.Count);
        for(int i=0;i<buildStateImageList.Count;i++)
        {
            if((1.0f/buildStateImageList.Count)*i<=buildStateProgress && buildStateProgress < (1.0f / buildStateImageList.Count)*(i+1))
            {
                buildStateImage.sprite = buildStateImageList[i];
                //Debug.Log("changeto");
            }
        }
    }
   
    public void BuildButtonPressed(string buildID)
    {
        foreach (var temp in techConstructList)
        {
            if (temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                return;
            }
        }
        foreach (var temp in utilityConstructList)
        {
            if(temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                return;
            }
        }
        foreach (var temp in combatConstructList)
        {
            if (temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                return;
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
    //public void TempGameStart()
    //{
    //    SetPlayer();
    //    PlayerManager.SetSpellType(new Dash());
    //    SceneManager.LoadScene("TestScene");
    //    
    //}
    public void GameStartButtonPressed()
    {
        SetPlayer();
        PlayerManager.SetSpellType(new Dash());
        DeactiveBasePanel();
    }
    public void DeactiveBasePanel()
    {
        basePanel.SetActive(false);
    }
    public void ActiveBasePanel()
    {
        basePanel.SetActive(true);
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
        switch (buildingInfo)
        {
            case TechBuildBase:
                if (buildingInfo.TryConstruct(techConstructList) == false)
                {
                    buildInfoBuildButton.interactable = false;
                }
                else
                {
                    buildInfoBuildButton.interactable = true;
                }
                break;
            case UtilityBuildBase:
                if (buildingInfo.TryConstruct(utilityConstructList) == false)
                {
                    buildInfoBuildButton.interactable = false;
                }
                else
                {
                    buildInfoBuildButton.interactable = true;
                }
                break;
            case CombatBuildBase:
                if (buildingInfo.TryConstruct(combatConstructList) == false)
                {
                    buildInfoBuildButton.interactable = false;
                }
                else
                {
                    buildInfoBuildButton.interactable = true;
                }
                break;
        }
        
    }

    //public bool TryGetPlayer()// ���� �Ⱦ���
    //{
    //    playerManager = FindObjectOfType<PlayerManager>();
    //    if (playerManager == null)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //
    //        return true;
    //    }
    //}
    public void SetPlayer()
    {
        if (isBuildEffectAplly == false)
        {
            //Debug.Log("player!");
            ActiveBuildEffect();
            isBuildEffectAplly = true;
        }
        //SetFinalStatusToPlayer();
    }

    //public void AddStatEffect(BuildEffect buildEffect)//����� �־����� ���� ������? 
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
          

        playerStatus = new PlayerStatus();// �̰� �׳� ���� �縸ŭ �غ��ϴ°� ��������? ���Ƴ��� �� ����
        playerStatus.moveSpeed += speedSum;
        playerStatus.maxHP += maxHPSum;
        playerStatus.gatheringSpeed += gatherSpeedSum;
        playerStatus.gatheringDelay += gatherDelaySum;
        playerStatus.playerDamage += damageSum;
        playerStatus.itemDetectionRange += gatherRangeSum;





    }
    public void ModifieUtillity(IUtilityBuildEffect buildEffect)
    {
        
    }
    public void ModifieSkill()//���� �������̽�?
    {

    }

    public void ActiveBuildEffect()
    {
        foreach (var building in techConstructList)
        {
            if (building.isBuildConstructed == true)
            {
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


        SetFinalStatusToPlayer();
    }

    public void SetOwnCost()
    {


        if (FirebaseDataBaseMgr.Instance == null)
        {
            //Debug.Log("firenull!");
            return;
        }
        else
        {
            
            FirebaseDataBaseMgr.Instance.StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(0));

            ownBuildCostDic.Add("Blueprint", FirebaseDataBaseMgr.Blueprint);
            ownBuildCostDic.Add("MetaCurrency", FirebaseDataBaseMgr.MetaCurrency);
            OnConstructCostChange.Invoke();

        }
        Debug.Log(FirebaseDataBaseMgr.Blueprint);
        Debug.Log(FirebaseDataBaseMgr.MetaCurrency);
        
        
    }
    public void SetFinalStatusToPlayer()
    {
        PlayerManager.PlayerStatus+=playerStatus;//���� �����ϸ� ���߿� ���ϴ°� ��������? ������ �����ε��� ���ְڴ�.


    }
    
    

}
