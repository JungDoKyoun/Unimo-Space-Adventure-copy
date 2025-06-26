namespace ZL.Unity.Unimo
{
    public interface IEnergizer
    {
        public int Energy { get; }

        public void GetEnergy(int value);
    }
}