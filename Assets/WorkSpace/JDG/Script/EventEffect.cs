using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;

namespace JDG
{
    public enum ChoiceEffectType
    {
        useful, harmful
    }

    [System.Serializable]
    public class EventEffect
    {
        public ChoiceEffectType _choiceEffectType;
        public TargetType _target;
        public float _value;
    }
}
