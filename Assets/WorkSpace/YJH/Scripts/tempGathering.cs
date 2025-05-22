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

    public void UseItem()//임시 채집물이라 별거 없음
    {
        //스테이지 매니저가 있다면 스테이지 매니저에 저장된 채집수 하나 늘리는 기능 -> 그래서 수가 일정 이상이면 스테이지 매니저가 승리 판단 
    }
}