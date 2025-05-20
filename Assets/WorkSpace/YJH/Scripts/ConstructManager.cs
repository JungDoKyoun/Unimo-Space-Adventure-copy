using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConstructManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints= new List<Transform>();
    [SerializeField] List<ConstructBase> constructList = new List<ConstructBase>();

    [SerializeField] GameObject buildingInfoPanel;
    [SerializeField] GameObject BuildPanel;
    [SerializeField] TMP_Text constructCostText;


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
    }

    private void Start()
    {
        
    }



}
