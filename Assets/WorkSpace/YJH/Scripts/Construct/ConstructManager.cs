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
    [SerializeField] Image buildStateImage;//�ǹ� �Ǽ� ��Ȳ�� ��Ÿ�� ��� �̹���
    [SerializeField] List<Image> buildingImages=new List<Image>();// �ǹ��� ���� ������ ������ �̹����� ������
    [SerializeField] List<Sprite> buildStateImageList= new List<Sprite>();//���߿� �Ǽ� �̹��� ��� �ٲ�� ������ �� 
    private Dictionary<int, int> imagePriority=new Dictionary<int, int>();
    private float buildStateProgress = 0;
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
    public GameObject initRelicUI;
    private void Awake()
    {
        
        Instance = this;
        OnConstructCostChange += SetConstructCostText;
        SetOwnCost();
        DecideProgress();//���߿� �̹��� ���� �ý��� �Ϻ��ϰ� �ٲٸ� �����ϱ�
        ToDictionary();
        SetAllDic();
        SetImagePriorityDicNum();
        SetAllConstructImages();
        if (isDelinkON == false)
        {
            PlayerManager.OnStageFail += YJH.MethodCollection.DelinkHealPlayer;
            PlayerManager.OnStageFail += ResetApplyBuildEffect;
            isDelinkON =true;
        }
    }
    //private void Start()
    //{
    //    if (tempRelic != null)
    //    {
    //        //PlayerInventoryManager.AddRelic(tempRelic);
    //        //Debug.Log(tempRelic.Effects[0].Value);
    //        //Debug.Log(PlayerInventoryManager.RelicDatas.Count);
    //    }
    //}
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
    public void SetImagePriorityDicNum()
    {
        for(int i=0;i<buildingImages.Count;i++)
        {
            imagePriority.Add(i, 0);
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
            TrySetConstructImage(building);
            //spawnPoints[building.spawnIndex].GetComponent<Image>().sprite = building.buildingImage;
            buildInfoBuildButton.interactable = false;
            int costNum;
            StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(building.BuildCostDic.TryGetValue("MetaCurrency", out costNum) ? -costNum : 0));
            //SetPlayer();
            DecideProgress();//���߿� �̹��� ��Ŀ���� �Ϻ��ϰ� �����ϸ� �ٲٱ�
            
            //��� ����Ʈ �Լ� �߰��ϱ�

            //�Ǽ��� �Ϸ�Ǿ��ٴ� ���̴ϱ� 
            //�Ǽ� �ݿ��� �ؾ� ��
            //��� �ؾ� �ұ�?
            //�Ǽ� ���̽��� ���� �ε����� �����ϱ� �� ���� �ε����� �̿��ؼ� ��������Ʈ�� �����ؼ� ���� ����Ʈ �ʿ� �ݿ�
            //�ݿ� �ϴ°� �� �г��� �����ϴ� ������� �̹����� �̿��ؼ� ���� �����? ������ �̹����� ������ �ٸ� �������� ����� �� �� ����
        
    }
    public void TrySetConstructImage(ConstructBase building)
    {
        if (imagePriority[building.imageIndex] < building.imagePriority)
        {
            buildingImages[building.imageIndex].sprite = building.buildingImage;
        }
    }

    public void SetAllConstructImages()
    {
        foreach(var temp in techConstructList)
        {
            if (temp.isBuildConstructed == true)
            {
                if (imagePriority[temp.imageIndex] < temp.imagePriority)
                {
                    buildingImages[temp.imageIndex].sprite = temp.buildingImage;
                }
            }
        }
        foreach (var temp in utilityConstructList)
        {
            if (temp.isBuildConstructed == true)
            {
                if (imagePriority[temp.imageIndex] < temp.imagePriority)
                {
                    buildingImages[temp.imageIndex].sprite = temp.buildingImage;
                }
            }
        }
        foreach (var temp in combatConstructList)
        {
            if (temp.isBuildConstructed == true)
            {
                if (imagePriority[temp.imageIndex] < temp.imagePriority)
                {
                    buildingImages[temp.imageIndex].sprite = temp.buildingImage;
                }
            }
        }
    }




    public void DecideProgress()//���߿� ���� ���� ���� �ʿ� -> ������ ��ũ��Ʈ�� ������ ĸ�ĸ� ���ؼ� ��ȭ �ݿ��ϴ� ������ 
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
   public void ResetApplyBuildEffect()
    {
        isBuildEffectAplly = false;
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
        //PlayerManager.SetSpellType(new Dash());//���߿� combat�迭 ���۽� ���� �ʿ�
        DeactiveBasePanel();
        //SceneManager.LoadScene("TestScene");
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
        buildInfoBuildButton.onClick.RemoveAllListeners();
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

    
    public void SetPlayer()// ���� ����� �������ͽ� �ʱ�ȭ �ʿ�
    {
        if (isBuildEffectAplly == false)//static ������ ���ؼ� �ʱ�ȭ ����
        {
            //Debug.Log("player!");
            playerStatus = PlayerManager.OriginStatus.Clone();
            ActiveBuildEffect();
            isBuildEffectAplly = true;
        }
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
          

        //playerStatus = PlayerManager.OriginStatus.Clone();// �̰� �׳� ���� �縸ŭ �غ��ϴ°� ��������? ���Ƴ��� �� ����-> Ŭ���� ���н� �ʱ�ȭ �ʿ��ϴ� ���������� ���ϴ� ������� ����
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
        //Debug.Log(FirebaseDataBaseMgr.Blueprint);
        //Debug.Log(FirebaseDataBaseMgr.MetaCurrency);
        
        
    }
    public static void SetFinalStatusToPlayer()
    {
        PlayerManager.PlayerStatus=playerStatus;//���� �ʱ�ȭ �����ϸ� ������ ������


    }
    
    public void ActiveInitRelic()
    {
        initRelicUI.SetActive(true);
        UIManager.Instance.IsUIOpen = true;
    }
    public void DeactiveInitRelic()
    {
        initRelicUI.SetActive(false);
        UIManager.Instance.IsUIOpen = false;
    }

}
