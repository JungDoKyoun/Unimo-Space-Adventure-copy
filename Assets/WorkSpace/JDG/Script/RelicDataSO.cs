using UnityEngine;
using JDG;

namespace JDG
{
    public enum RelicEffectType
    {
        None
    }

    [CreateAssetMenu(fileName = "RelicDataSO", menuName = "SO/EventSO/RelicDataSO")]
    public class RelicDataSO : ScriptableObject
    {
        public string _relicName;
        public Sprite _relicImage;
        public string _relicDesc;
        public  ResourceCostSO _relicPrice;
        public RelicEffectType _relicEffectType;
        public float _relicEffectValue;
    }
}
