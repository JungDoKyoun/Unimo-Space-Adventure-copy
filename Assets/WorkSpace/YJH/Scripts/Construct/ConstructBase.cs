using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildCost
{
    public string key;
    public int value;
}


[CreateAssetMenu(fileName = "ConstructInfo", menuName = "ScriptableObject/Construct")]
public class ConstructBase :ScriptableObject//,IConstruct
{
    public bool isBuildConstructed;
    public string buildID;
    public string buildName;
    public List<string> buildRequires=new List<string>();//필요한 건물 buildid
    public List<string> buildUnlocks=new List<string>();
    public List<BuildCost> buildCosts=new List<BuildCost>();//이 건물이 필요한 코스트
    private Dictionary<string, int> buildCostDic=new Dictionary<string, int>();//별도 처리 필요
    public Dictionary<string, int> BuildCostDic {  get { return buildCostDic; } }


    //public string buildEffectDirection;//이거 addressable아니면 resource.load인데 애드레서블은 사실상 사용을 못하고 resource는 많이 사용하면 메모리 먹음 그냥 직접 대입이 맞을듯
    public Vector3 buildPosition=new Vector3();
    public Sprite buildIcon;
    public Sprite buildingImage;
    public string buildType;
    public bool isbuildRepeatable;
    //public string buildPrefabDirection;//이것도 직접이 맞을듯 
    public List<BuildEffect> buildEffects=new List<BuildEffect>();
    public int spawnIndex;
    
    public string buildingDescription;


    public void Init()//현재는 안씀?
    {
        
    }
    
    public bool IsBuildConstructed()
    {
        return isBuildConstructed;
    }
    
    public Dictionary<string,int> ToDictionary()
    {
        var dict = new Dictionary<string, int>();
        foreach (var pair in buildCosts)
        {
            dict[pair.key] = pair.value;
        }
        return buildCostDic= dict;
    }
    
    public void ConstructEnd()
    {
        Debug.Log("buildcomplete");
        isBuildConstructed = true;
    }
    public bool TryConstruct()
    {
        if (IsRequiredFulFilled() == false)
        {
            Debug.Log("buildrequire");
            return false;
        }
        if (IsBuildCostEnough(ConstructManager.Instance.OwnBuildCostDic)==false)//딕셔너리 파이어베이스 받아서 넣기
        {
            Debug.Log("notenoughcost");
            return false;
        }
        if (isBuildConstructed == true && isbuildRepeatable == false)
        {
            Debug.Log("already builded");
            return false;
        }
        //ConstructEnd();
        
        return true;
    }
    public bool IsBuildCostEnough(Dictionary<string,int> ownCostDic)// 이쪽 수정 필요
    {
        foreach(var pair in buildCostDic)
        {
            if (ownCostDic.ContainsKey(pair.Key) == false)
            {
                Debug.Log("no cost");
                return false;
            }
            if (ownCostDic[pair.Key]<pair.Value)
            {
                Debug.Log("notenough cost");
                return false;
            }
        }
        return true;
    }

    public bool IsRequiredFulFilled()
    {
        foreach(var a in ConstructManager.Instance.ConstructList)
        {
            foreach (var b in buildRequires)
            {
                if (a.buildID == b)
                {
                    if (a.isBuildConstructed == false)
                    {
                        return false;
                    }
                }
            }
        }

        return true; 


    }
    
   
    
}
