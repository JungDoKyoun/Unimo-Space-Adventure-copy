using System;

namespace ZL.Unity.Collections
{
    [Serializable]

    public sealed class Wrapper<TValue>
    {
        public TValue value;
    }
}