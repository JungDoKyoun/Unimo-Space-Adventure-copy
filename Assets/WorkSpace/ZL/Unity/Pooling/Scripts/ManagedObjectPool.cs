using System;

using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class ManagedObjectPool<TKey, TClone> : ObjectPool<TClone>

        where TClone : Component, IKeyValuePair<TKey, TClone>
    {
        private readonly Dictionary<TKey, TClone> clones = new Dictionary<TKey, TClone>();

        public TClone this[TKey key]
        {
            get => clones[key];
        }

        public bool TryGenerate(TKey key, out TClone clone)
        {
            if (clones.ContainsKey(key) == true)
            {
                clone = clones[key];

                return false;
            }

            clone = Cloning();

            clone.Key = key;

            clones.Add(key, clone);

            return true;
        }

        public TClone Find(TKey key)
        {
            return clones[key];
        }

        public override void Collect(TClone clone)
        {
            clones.Remove(clone.Key);

            base.Collect(clone);
        }

        public void CollectAll()
        {
            foreach (var kvp in clones.Values.ToArray())
            {
                kvp.gameObject.SetActive(false);
            }

            clones.Clear();
        }

        public void ReleaseAll()
        {

        }
    }

    [Serializable]

    public class ManagedObjectPool<TClone> : ObjectPool<TClone>

        where TClone : Component
    {
        private readonly HashSet<TClone> clones = new HashSet<TClone>();

        public override TClone Cloning()
        {
            var clone = base.Cloning();

            clones.Add(clone);

            return clone;
        }

        public override void Collect(TClone clone)
        {
            clones.Remove(clone);

            base.Collect(clone);
        }

        public void CollectAll()
        {
            foreach (var clone in clones.ToArray())
            {
                clone.gameObject.SetActive(false);
            }

            clones.Clear();
        }
    }
}