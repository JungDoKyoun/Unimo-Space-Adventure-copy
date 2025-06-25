using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Extensions;
using System.Threading.Tasks;
using Unity.VisualScripting;
using ZL.Unity.Unimo;
using JDG;
using ZL.CS.Singleton;
using YJH;
public class ConstructUIManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject buildingInfoPanel;
    public Image buildingImage;
    public TMP_Text buildingTitleText;
    public TMP_Text buildingInfoText;
    public TMP_Text buildingRequireText;
    public TMP_Text buildingCostText;
    public TMP_Text constructCostText;
    public Button buildInfoBuildButton;
    public GameObject basePanel;

    public Sprite buildedSprite;
    public Sprite cannotBuildSprite;
    public Sprite canBuildSprite;

    public List<Button> allButtons;
    
    public GameObject techBuildButtons;
    public GameObject utilBuildButtons;
    public GameObject combatBuildButtons;
    

    [Header("�Ǽ��Ϸ� ȭ�� ����")]
    public Image buildStateImage;//�ǹ� �Ǽ� ��Ȳ�� ��Ÿ�� ��� �̹���
    public List<Image> buildingImages = new List<Image>();// �ǹ��� ���� ������ ������ �̹����� ������

    public GameObject initRelicUI;
    public static ConstructUIManager Instance { get; private set; }
    public Dictionary<int, int> imagePriority = new Dictionary<int, int>();

    public GameObject constructManager;//������ 
    public AudioSource constructUIAudioSource;
    public AudioClip buildSuccessClip;

    public CanvasGroup stationCanvasGroup;
    
    private void Awake()
    {
        Instance = this;
        SetImagePriorityDicNum();
        if (ConstructManager.Instance == null)
        {
            Instantiate(constructManager);
        }
        SetAllButtons();
        ChangeButtonsSpriteAll();
    }

    public void SetAllButtons()
    {
        allButtons = new List<Button>();
        Button[] techButtons= techBuildButtons.GetComponentsInChildren<Button>(includeInactive: true);
        Button[] utilButtons= utilBuildButtons.GetComponentsInChildren<Button>(includeInactive: true);
        Button[] combatButtons= combatBuildButtons.GetComponentsInChildren<Button>(includeInactive: true);
        foreach (Button button in techButtons)
        {
            allButtons.Add(button);
        }
        foreach (Button button in utilButtons)
        {
            allButtons.Add(button);
        }
        foreach(Button button in combatButtons)
        {
            allButtons.Add(button);
        }



    }
    public void ChangeButtonsSpriteAll()
    {
        if(allButtons == null)
        {
            return;
        }
        if(allButtons.Count == 0)
        {
            return;
        }

        foreach( Button button in allButtons)
        {
            var tempScript = button.GetComponent<ButtonStringHolder>();
            var tempBuildData = ConstructManager.Instance.AllBuildingDic[tempScript.BuildingID];
            if(tempBuildData == null)
            {
                return;
            }
            if (tempBuildData.isBuildConstructed == true)//�̹� �Ǽ��Ǿ����� 
            {
                button.gameObject.GetComponent<Image>().sprite=buildedSprite;
            }
            else
            {
                switch (tempBuildData)
                {
                    case TechBuildBase:
                        if (tempBuildData.TryConstruct(ConstructManager.Instance.techConstructList) == false)//�ǹ��� ���� �� ������
                        {
                            button.gameObject.GetComponent<Image>().sprite = cannotBuildSprite;
                        }
                        else//�ǹ��� ���� ���� ���� �� ���� ����
                        {
                            button.gameObject.GetComponent<Image>().sprite = canBuildSprite;
                        }

                            break;
                    case UtilityBuildBase:
                        if (tempBuildData.TryConstruct(ConstructManager.Instance.utilityConstructList) == false)
                        {
                            button.gameObject.GetComponent<Image>().sprite = cannotBuildSprite;
                        }
                        else
                        {
                            button.gameObject.GetComponent<Image>().sprite = canBuildSprite;
                        }
                            break;
                    case CombatBuildBase:
                        if (tempBuildData.TryConstruct(ConstructManager.Instance.combatConstructList) == false)
                        {
                            button.gameObject.GetComponent<Image>().sprite = cannotBuildSprite;
                        }
                        else
                        {
                            button.gameObject.GetComponent<Image>().sprite = canBuildSprite;
                        }
                            break;
                    default:
                        Debug.Log("non build type");
                        return;

                }
            }





        }



    }
    
    public void DeactiveBasePanel()
    {
        basePanel.SetActive(false);
    }
    public void ActiveBasePanel()
    {
        //Debug.Log("�Ǽ� ȭ�� ����");
        basePanel.SetActive(true);
        //Debug.Log(FirebaseDataBaseMgr.Blueprint);
        //Debug.Log(FirebaseDataBaseMgr.MetaCurrency);
        ConstructManager.Instance.SetOwnCost();
        //StartCoroutine(FirebaseDataBaseMgr.Instance.UpdateRewardMetaCurrency(10000));//����� �ӽ� �Լ�
        //Debug.Log("����� �ӽ� �ڿ� �߰�");
    }
    public void EndConstructButtonPressed()
    {
        DeactiveBasePanel();
    }

    
    public void DeActiveBuildInfoPanel()
    {
        buildingInfoPanel.SetActive(false);
    }
    public void ActiveInitRelic()
    {
        initRelicUI.SetActive(true);
        ConstructManager.Instance.isGiveStartRellic = false;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.IsUIOpen = true;
        }
    }
    public void DeactiveInitRelic()
    {
        initRelicUI.SetActive(false);
        if (UIManager.Instance != null)
        {
            UIManager.Instance.IsUIOpen = false;
        }
    }
    public void GameStartButtonPressed()//���� �÷��̾� ��� �� ���ư��� �Ǽ� UI�� ������ ���� �Ƹ� ������ ���ؼ� �׷���?
    {
        //Debug.Log("�÷��̾�� �Ǽ�ȿ�� ���� ����:" + ConstructManager.IsBuildEffectAplly);
        ConstructManager.Instance.SetPlayer();
        PlayerManager.ResetStatus();
        //Debug.Log("�Ǽ��Ŵ����� ���� �ʱ�ȭ ��Ŵ");
        InitRelicGiver.Instance.SetRelicData();
        //PlayerManager.SetSpellType(new Dash());//���߿� combat�迭 ���۽� ���� �ʿ�
        if (ConstructManager.Instance.isGiveStartRellic == true)
        {
            ActiveInitRelic();
        }

        //SceneManager.LoadScene("TestScene");
    }
    public void SetImagePriorityDicNum()
    {
        for (int i = 0; i < buildingImages.Count; i++)
        {
            imagePriority.Add(i, 0);
        }
        imagePriority.Add(-1, 999);
    }
    public void TrySetConstructImage(ConstructBase building)
    {
        if (imagePriority[building.imageIndex-1] < building.imagePriority)
        {
            buildingImages[building.imageIndex-1].sprite = building.buildingImage;
        }
    }

    //public void ChangeBuildStateImage()
    //{
    //    //Debug.Log(buildStateImageList.Count);
    //    for (int i = 0; i < buildStateImageList.Count; i++)
    //    {
    //        if ((1.0f / buildStateImageList.Count) * i <= buildStateProgress && buildStateProgress < (1.0f / buildStateImageList.Count) * (i + 1))
    //        {
    //            buildStateImage.sprite = buildStateImageList[i];
    //            //Debug.Log("changeto");
    //        }
    //    }
    //}
    public void ActivePanel(GameObject panel)
    {
        panel.SetActive(true);
    }
    public void DeactivePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void DisappearButtons()
    {
        stationCanvasGroup.alpha = 0;
    }
    public void AppearButtons()
    {
        stationCanvasGroup.alpha = 1;
    }
    public void BuildButtonPressed(string buildID)
    {
        foreach (var temp in ConstructManager.Instance.techConstructList)
        {
            if (temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                return;
            }
        }
        foreach (var temp in ConstructManager.Instance.utilityConstructList)
        {
            if (temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                return;
            }
        }
        foreach (var temp in ConstructManager.Instance.combatConstructList)
        {
            if (temp.buildID == buildID)
            {
                ShowBuildInfoPanel(temp);
                return;
            }
        }
    }
    public void ShowBuildInfoPanel(ConstructBase buildingInfo)
    {
        var requireText = "";
        buildingInfoPanel.SetActive(true);
        buildingTitleText.text = buildingInfo.buildName;
        //buildingInfoText.text=
        foreach (var temp in buildingInfo.buildRequires)
        {
            requireText += " " + temp;
        }
        string costText = "";
        buildingRequireText.text = requireText;
        foreach (var temp in buildingInfo.BuildCostDic)
        {
            costText += "\"" + temp.Key + "\"" + ":" + temp.Value.ToString() + "";

        }
        buildingCostText.text = costText;
        buildingInfoText.text = buildingInfo.buildingDescription;
        buildingImage.sprite = buildingInfo.buildIcon;
        ConstructManager.Instance.DecideCanBuild(buildingInfo);
        buildInfoBuildButton.onClick.RemoveAllListeners();
        buildInfoBuildButton.onClick.AddListener(() => ConstructManager.Instance.TryConstruct(buildingInfo));




    }
    public void ConstructSuccessSoundPlay()
    {
        if (constructUIAudioSource != null)
        {
            constructUIAudioSource.clip = buildSuccessClip;

            constructUIAudioSource.Play();
        }
        else
        {
            return;
        }
    }
    


}
