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


    [Header("�Ǽ��Ϸ� ȭ�� ����")]
    public Image buildStateImage;//�ǹ� �Ǽ� ��Ȳ�� ��Ÿ�� ��� �̹���
    public List<Image> buildingImages = new List<Image>();// �ǹ��� ���� ������ ������ �̹����� ������

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
        Debug.Log("�Ǽ� ȭ�� ����");
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
    public void GameStartButtonPressed()//���� �÷��̾� ��� �� ���ư��� �Ǽ� UI�� ������ ���� �Ƹ� ������ ���ؼ� �׷���?
    {
        Debug.Log("�÷��̾�� �Ǽ�ȿ�� ���� ����:" + ConstructManager.IsBuildEffectAplly);
        ConstructManager.Instance.SetPlayer();
        PlayerManager.ResetStatus();
        Debug.Log("�Ǽ��Ŵ����� ���� �ʱ�ȭ ��Ŵ");
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
