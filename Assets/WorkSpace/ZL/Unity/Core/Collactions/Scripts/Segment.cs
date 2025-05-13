using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity.Collections
{
    [Serializable]

    public struct Segment<TEquatable> : IEquatable<Segment<TEquatable>>

        where TEquatable : IEquatable<TEquatable>
    {
        [SerializeField]

        private TEquatable start;

        public readonly TEquatable Start
        {
            get => start;
        }

        [SerializeField]

        private TEquatable end;

        public readonly TEquatable End
        {
            get => end;
        }

        public Segment(TEquatable start, TEquatable end)
        {
            this.start = start;

            this.end = end;
        }

        public override bool Equals(object obj)
        {
            if (obj is Segment<TEquatable> other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(Segment<TEquatable> other)
        {
            return (start.Equals(other.start) && end.Equals(other.end)) || (start.Equals(other.end) && end.Equals(other.start));
        }

        public override readonly int GetHashCode()
        {
            return start.GetHashCode() ^ end.GetHashCode();
        }

        public override readonly string ToString()
        {
            return $"[{start}, {end}]";
        }

        public sealed class EqualityComparer : IEqualityComparer<Segment<TEquatable>>
        {
            public bool Equals(Segment<TEquatable> x, Segment<TEquatable> y)
            {
                return x.Equals(y);
            }

            public int GetHashCode(Segment<TEquatable> obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}