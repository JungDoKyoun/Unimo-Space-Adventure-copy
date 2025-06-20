using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Unimo
{
    [Serializable]

    public sealed class RelicEffect
    {
        [Space]

        [SerializeField]

        private RelicEffectType type = RelicEffectType.None;

        public RelicEffectType Type
        {
            get => type;
        }

        [SerializeField]

        private float value = 0f;

        public float Value
        {
            get => value;
        }

        private object[] args = null;

        public object[] Args
        {
            get
            {
                if (args == null)
                {
                    args = new object[2];

                    args[0] = RelicEffectStringTableSheet.Instance[type].Value;

                    args[1] = value;
                }

                return args;
            }
        }

        public static RelicEffect Parse(string s)
        {
            var strings = s.Split(',');

            return new()
            {
                type = (RelicEffectType)int.Parse(strings[0]),

                value = float.Parse(strings[1]),
            };
        }

        public override string ToString()
        {
            return $"{(int)type}, {value}";
        }

        public void Refresh()
        {
            Args[0] = RelicEffectStringTableSheet.Instance[type].Value;
        }
    }
}