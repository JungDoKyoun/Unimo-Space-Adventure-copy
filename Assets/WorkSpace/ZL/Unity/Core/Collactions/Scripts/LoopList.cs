using System.Collections.Generic;

using System;

using UnityEngine;

using ZL.CS;

namespace ZL.Unity.Collections
{
    [Serializable]

    public sealed class LoopList<T>
    {
        [SerializeField]

        private int index = 0;

        public int Index
        {
            get => index;

            set => index = value;
        }

        [SerializeField]

        private LoopPattern loopPattern = LoopPattern.Clamp;

        [Space]

        [SerializeField]

        private List<T> list = new List<T>();

        public int Count
        {
            get => list.Count;
        }

        public bool TryGetCurrent(out T current)
        {
            if (list.Count == 0)
            {
                current = default;

                return false;
            }

            current = list[index.Loop(0, Count - 1, loopPattern)];

            return true;
        }
    }
}