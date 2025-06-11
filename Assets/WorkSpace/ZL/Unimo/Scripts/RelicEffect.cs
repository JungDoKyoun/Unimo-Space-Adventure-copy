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

        private List<object> args = null;

        public List<object> Args
        {
            get
            {
                if (args == null)
                {
                    args = new()
                    {
                        RelicEffectStringTableSheet.Instance[type].Value
                    };

                    if (value != 0f)
                    {
                        args.Add(value);
                    }
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
    }
}