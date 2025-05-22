using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConstructManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints= new List<Transform>();
    [SerializeField] List<ConstructBase> constructList = new List<ConstructBase>();

    [Header("UI")]
    [SerializeField] GameObject buildingInfoPanel;
    [SerializeField] Image buildingImage;
    [SerializeField] TMP_Text buildingTitleText;
    [SerializeField] TMP_Text buildingInfoText;
    [SerializeField] TMP_Text buildingRequireText;
    [SerializeField] TMP_Text buildingCostText;
    
    [SerializeField] GameObject BuildPanel;
    [SerializeField] TMP_Text constructCostText;

    [SerializeField] List<Button> buildButtons = new List<Button>();
    [SerializeField] Button buildInfoBuildButton;

    public List<ConstructBase> ConstructList { get { return constructList;  } private set { constructList = value; } }
    public static ConstructManager Instance { get; private set; }

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

        ToDictionary();
    }

    public void ToDictionary()
    {
        foreach(var temp in constructList)
        {
            temp.ToDictionary();
        }
    }
    public void Init()
    {
        foreach (var temp in constructList)
        {
            temp.Init();
        }
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

    


    public void ShowBuildPanel()
    {
        BuildPanel.SetActive(true); 
    }
    public void DeActiveBuildPanel()
    {
        BuildPanel.SetActive(false);
    }
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
        foreach (var temp in buildingInfo.buildCostDic)
        {
            costText += "\""+temp.Key+"\""+":"+temp.Value.ToString()+"";
            
        }
        buildingCostText.text = costText;
        buildingInfoText.text=buildingInfo.buildingDescription;
        DecideCanBuild( buildingInfo);
            



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
    

    


}
