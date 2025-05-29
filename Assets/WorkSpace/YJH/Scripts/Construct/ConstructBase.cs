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
    public List<string> buildRequires=new List<string>();//�ʿ��� �ǹ� buildid
    public List<string> buildUnlocks=new List<string>();
    public List<BuildCost> buildCosts=new List<BuildCost>();//�� �ǹ��� �ʿ��� �ڽ�Ʈ
    private Dictionary<string, int> buildCostDic=new Dictionary<string, int>();//���� ó�� �ʿ�
    public Dictionary<string, int> BuildCostDic {  get { return buildCostDic; } }


    //public string buildEffectDirection;//�̰� addressable�ƴϸ� resource.load�ε� �ֵ巹������ ��ǻ� ����� ���ϰ� resource�� ���� ����ϸ� �޸� ���� �׳� ���� ������ ������
    public Vector3 buildPosition=new Vector3();
    public Sprite buildIcon;
    public Sprite buildingImage;
    public string buildType;
    public bool isbuildRepeatable;
    //public string buildPrefabDirection;//�̰͵� ������ ������ 
    public List<BuildEffect> buildEffects=new List<BuildEffect>();
    public int spawnIndex;
    
    public string buildingDescription;


    public void Init()//����� �Ⱦ�?
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
        if (IsBuildCostEnough(ConstructManager.Instance.OwnBuildCostDic)==false)//��ųʸ� ���̾�̽� �޾Ƽ� �ֱ�
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
    public bool IsBuildCostEnough(Dictionary<string,int> ownCostDic)// ���� ���� �ʿ�
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
