using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public enum RelicEffectType
    {
        None
    }

    public enum TargetType
    {
        None
    }

    [CreateAssetMenu(fileName = "RelicDataSO", menuName = "SO/EventSO/RelicDataSO")]
    public class RelicDataSO : ScriptableObject
    {
        public string _relicName;
        public Sprite _relicImage;
        public string _relicDesc;
        public ResourceCost _relicPrice;
        public List<RelicEffect> _relicEffects;
    }

    [System.Serializable]
    public class RelicEffect
    {
        public RelicEffectType _effectType;
        public TargetType _target;
        public float _value;
    }
}
