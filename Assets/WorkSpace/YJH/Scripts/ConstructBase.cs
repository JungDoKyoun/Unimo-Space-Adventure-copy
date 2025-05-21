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
    public Dictionary<string, int> buildCostDic=new Dictionary<string, int>();//별도 처리 필요
    public string buildEffectDirection;
    public Vector3 buildPosition=new Vector3();
    public string buildIconDirection;
    public string buildType;
    public bool isbuildRepeatable;
    public string buildPrefabDirection;
    public List<BuildEffect> buildEffects=new List<BuildEffect>();
    public int spawnIndex;
    
    public string buildingDescription;


    
    
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
        isBuildConstructed = true;
    }
    public bool TryConstruct()
    {
        if (IsRequiredFulFilled() == false)
        {
            return false;
        }
        if (IsBuildCostEnough(new Dictionary<string, int>()) == false)
        {
            Debug.Log("notenoughcost");
            return false;
        }
        if (isBuildConstructed == true && isbuildRepeatable == false)
        {
            return false;
        }
        //ConstructEnd();
        return true;
    }
    public bool IsBuildCostEnough(Dictionary<string,int> ownCostDic)
    {
        foreach(var pair in buildCostDic)
        {
            if (ownCostDic.ContainsKey(pair.Key) == false)
            {
                return false;
            }
            if (ownCostDic[pair.Key]<pair.Value)
            {
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
                    if (isBuildConstructed == false)
                    {
                        return false;
                    }
                }
            }
        }

        return true; 


    }
    
   public void ActiveBuildingEffect()
    {
        foreach(var t in buildEffects)
        {
            //t.playerStatus.Duplicate()//게임 매니저의 플레이어 스테이터스 할당
        }

        foreach (var t in buildEffects)
        {
            t.ApplyBuildEffect();
        }

    }
    
}
