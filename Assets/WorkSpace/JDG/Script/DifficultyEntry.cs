using JDG;

namespace JDG
{
    public enum DifficultyType
    {
        None, Easy, Normal, Hard
    }

    [System.Serializable]
    public class DifficultyEntry
    {
        public DifficultyType DifficultyType;
        public int Distance;
    }
}
