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
public class ConstructBase :ScriptableObject
{
    public bool isBuildConstructed;
    public string buildID;
    public string buildName;
    public List<string> buildRequires=new List<string>();//�ʿ��� �ǹ� buildid
    public List<string> buildUnlocks=new List<string>();
    public List<BuildCost> buildCosts=new List<BuildCost>();//�� �ǹ��� �ʿ��� �ڽ�Ʈ
    public Dictionary<string, int> buildCostDic=new Dictionary<string, int>();//���� ó�� �ʿ�
    public string buildEffect;
    public Vector3 buildPosition=new Vector3();
    public string buildIconDirection;
    public string buildType;
    public bool buildRepeatable;
    public string buildPrefabDirection;
    public int spawnIndex;

    

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
        if (IsBuildCostEnough(buildCostDic/*���� �ӽ÷� ���� ��ųʸ��� ���߿� ��ȭ�� �����ϱ�*/) == false)
        {
            return false;
        }
        //ConstructEnd();
        return true;
    }
    public bool IsBuildCostEnough(Dictionary<string,int> ownCostDic)
    {
        foreach(var pair in ownCostDic)
        {
            if (buildCostDic[pair.Key]>pair.Value)
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
}
