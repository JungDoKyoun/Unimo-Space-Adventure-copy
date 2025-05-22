using UnityEngine;

public class tempGathering : MonoBehaviour, IGatheringObject
{
    [SerializeField]
    
    private float hp;

    [SerializeField]

    private float maxHp;

    [SerializeField]

    private int gatheringPoint;

    public float CurrentHealth
    {
        get { return hp; }

        set { hp = value; }
    }

    public float MaxHealth
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
}