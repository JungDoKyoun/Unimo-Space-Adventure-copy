public interface IGatheringObject 
{
    public float CurrentHealth { get; set; }

    public float MaxHealth { get; }

    public int ActiveCount { get; set; }

    public void UseItem();

    public void BeGathering();

    public IGatheringObject ReturnSelf();

    public void OnGatheringEnd();
}