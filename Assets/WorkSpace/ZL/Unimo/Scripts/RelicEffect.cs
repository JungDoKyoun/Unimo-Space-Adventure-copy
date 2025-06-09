using System;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    [Serializable]

    public struct RelicEffect
    {
        [Space]

        [SerializeField]

        private RelicEffectType type;

        public RelicEffectType Type
        {
            get => type;
        }

        [SerializeField]

        private float value;

        public float Value
        {
            get => value;
        }

        public static RelicEffect Parse(string s)
        {
            var strings = s.Split(',');

            return new RelicEffect()
            {
                type = (RelicEffectType)int.Parse(strings[0]),

                value = float.Parse(strings[1]),
            };
        }

        public override readonly string ToString()
        {
            return $"{(int)type}, {value}";
        }
    }
}