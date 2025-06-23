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


    [Header("건설완료 화면 관련")]
    public Image buildStateImage;//건물 건설 현황을 나타낼 배경 이미지
    public List<Image> buildingImages = new List<Image>();// 건물을 지을 때마다 갱신할 이미지들 모음집

    public GameObject initRelicUI;
    public static ConstructUIManager Instance { get; private set; }
    public Dictionary<int, int> imagePriority = new Dictionary<int, int>();



    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetImagePriorityDicNum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactiveBasePanel()
    {
        basePanel.SetActive(false);
    }
    public void ActiveBasePanel()
    {
        Debug.Log("건설 화면 등장");
        basePanel.SetActive(true);
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
    public void GameStartButtonPressed()//현재 플레이어 사망 후 돌아가면 건설 UI가 열리지 않음 아마 오류로 인해서 그런듯?
    {
        Debug.Log("플레이어에게 건설효과 적용 여부:" + ConstructManager.IsBuildEffectAplly);
        ConstructManager.Instance.SetPlayer();
        PlayerManager.ResetStatus();
        Debug.Log("건설매니저가 스탯 초기화 시킴");
        InitRelicGiver.Instance.SetRelicData();
        //PlayerManager.SetSpellType(new Dash());//나중에 combat계열 제작시 변경 필요
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
    }
    public void TrySetConstructImage(ConstructBase building)
    {
        if (imagePriority[building.imageIndex] < building.imagePriority)
        {
            buildingImages[building.imageIndex].sprite = building.buildingImage;
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
            if (temp.buildID == buildID)
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
}
