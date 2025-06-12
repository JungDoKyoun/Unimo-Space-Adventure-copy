using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JDG;
using ZL.Unity.Unimo;

namespace JDG
{
    public enum ChoiceEffectType
    {
        None , Useful, Harmful
    }

    public enum EffectType
    {
        None, ChangeResource, ChangeRelic, ChangeMaxHP, ChangeCurrentHP, ChangeMaxFuel, ChangeCurrentFuel
    }

    [System.Serializable]
    public class EventEffect
    {
        public ChoiceEffectType _choiceEffectType;
        public EffectType _effectType;
        public TargetType _target;
        public int _value;

        [Header("�̺�Ʈ�� ������ ������")]
        public RelicData _relicData;
    }
}
