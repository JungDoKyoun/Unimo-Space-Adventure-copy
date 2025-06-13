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
    public string buildingDescription;
    public List<string> buildRequires=new List<string>();//필요한 건물 buildid
    public List<string> buildUnlocks=new List<string>();
    public List<BuildCost> buildCosts=new List<BuildCost>();//이 건물이 필요한 코스트
    private Dictionary<string, int> buildCostDic=new Dictionary<string, int>();//별도 처리 필요
    public Dictionary<string, int> BuildCostDic {  get { return buildCostDic; } }


    //public string buildEffectDirection;//이거 addressable아니면 resource.load인데 애드레서블은 사실상 사용을 못하고 resource는 많이 사용하면 메모리 먹음 그냥 직접 대입이 맞을듯
    public int imageIndex;//어떤 이미지에 덮어씌울지 사용하는 용도
    public Sprite buildIcon;//여기 나온 이미지를 그대로 백그라운드에 적용시키는 방식으로 진행하자 같은 단계가 올라가면 건물이 지어지는 느낌으로 util의 경우에는 패스 할까? 
    public Sprite buildingImage;
    public int imagePriority;
    public string buildType;
    public bool isbuildRepeatable;
    //public string buildPrefabDirection;//이것도 직접이 맞을듯 
    //public List<BuildEffect> buildEffects=new List<BuildEffect>();
    //public int spawnIndex;
    
    //public string buildingDescription;


    
    
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
    public bool TryConstruct(List<ConstructBase> constructBases)
    {
        if (IsRequiredFulFilled(constructBases) == false)
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
    public bool TryConstruct(List<TechBuildBase> constructBases)
    {
        if (IsRequiredFulFilled(constructBases) == false)
        {
            Debug.Log("buildrequire");
            return false;
        }
        if (IsBuildCostEnough(ConstructManager.Instance.OwnBuildCostDic) == false)//딕셔너리 파이어베이스 받아서 넣기
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
    public bool TryConstruct(List<UtilityBuildBase> constructBases)
    {
        if (IsRequiredFulFilled(constructBases) == false)
        {
            Debug.Log("buildrequire");
            return false;
        }
        if (IsBuildCostEnough(ConstructManager.Instance.OwnBuildCostDic) == false)//딕셔너리 파이어베이스 받아서 넣기
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
    public bool TryConstruct(List<CombatBuildBase> constructBases)
    {
        if (IsRequiredFulFilled(constructBases) == false)
        {
            Debug.Log("buildrequire");
            return false;
        }
        if (IsBuildCostEnough(ConstructManager.Instance.OwnBuildCostDic) == false)//딕셔너리 파이어베이스 받아서 넣기
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

    public bool IsRequiredFulFilled(List<ConstructBase> constructBases )
    {
        foreach(var a in constructBases)
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
                else if(b.Trim()=="")
                {
                    continue;
                }
            }
        }

        return true; 


    }
    public bool IsRequiredFulFilled(List<TechBuildBase> constructBases)
    {
        foreach (var a in constructBases)
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
    public bool IsRequiredFulFilled(List<UtilityBuildBase> constructBases)
    {
        foreach (var a in constructBases)
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
    public bool IsRequiredFulFilled(List<CombatBuildBase> constructBases)
    {
        foreach (var a in constructBases)
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
