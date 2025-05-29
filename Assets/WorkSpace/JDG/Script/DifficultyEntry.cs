using JDG;

namespace JDG
{
    public enum DifficultyType
    {
        Easy, Normal, Hard
    }

    [System.Serializable]
    public class DifficultyEntry
    {
        public DifficultyType _difficultyType;
        public int _distance;
    }
}
