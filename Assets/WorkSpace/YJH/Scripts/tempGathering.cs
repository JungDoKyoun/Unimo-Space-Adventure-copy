using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempGathering : MonoBehaviour,IGatheringObject
{

    [SerializeField] float hp;
    [SerializeField] float maxHp;
    [SerializeField] int gatheringPoint;
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
        get { return gatheringPoint; }
        set {  gatheringPoint = value; }
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
        //�������� �Ŵ����� �ִٸ� �������� �Ŵ����� ����� ä���� �ϳ� �ø��� ��� -> �׷��� ���� ���� �̻��̸� �������� �Ŵ����� �¸� �Ǵ� 
    }


    // Start is called before the first frame update





}
