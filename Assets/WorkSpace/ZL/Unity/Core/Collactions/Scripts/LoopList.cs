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

        [SerializeField]

        private List<T> items = new List<T>();

        public int Count
        {
            get => items.Count;
        }

        public bool TryGetCurrent(out T current)
        {
            if (Count != 0)
            {
                current = items[index.Loop(0, Count - 1, loopPattern)];

                return true;
            }

            current = default;

            return false;
        }
    }
}