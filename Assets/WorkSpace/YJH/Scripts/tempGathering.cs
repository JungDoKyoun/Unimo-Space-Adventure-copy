using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempGathering : MonoBehaviour,IGatheringObject
{

    [SerializeField] float hp;
    [SerializeField] float maxHp;
    //[SerializeField] float minHp;
    public float NowHP
    {
        get { return hp; }
        set { hp = value; }
    }
    public float MaxHP
    {
        get { return maxHp; }

    }

    public int ActiveCount 
    {
        get;
        set;
    }

    public void BeGathering()
    {
        
    }

    public void OnGatheringEnd()
    {
        UseItem();
        Destroy(gameObject);
    }

    public IGatheringObject ReturnSelf()
    {
        return this;
    }

    public void UseItem()//�ӽ� ä�����̶� ���� ����
    {
        
    }


    // Start is called before the first frame update





}
