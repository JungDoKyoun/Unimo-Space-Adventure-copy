using System;

using System.Collections.Generic;

using System.Linq;

using UnityEngine;

using ZL.Unity.Collections;

namespace ZL.Unity.Pooling
{
    [Serializable]

    public sealed class ManagedObjectPool<TKey, TComponent> : ObjectPool<TComponent>

        where TComponent : Component, IKeyValuePair<TKey, TComponent>
    {
        private readonly Dictionary<TKey, TComponent> replicas = new Dictionary<TKey, TComponent>();

        public TComponent this[TKey key]
        {
            get => replicas[key];
        }

        public bool TryGenerate(TKey key, out TComponent replica)
        {
            if (replicas.ContainsKey(key) == true)
            {
                replica = replicas[key];

                return false;
            }

            replica = Generate();

            replica.Key = key;

            replicas.Add(key, replica);

            return true;
        }

        public TComponent Find(TKey key)
        {
            return replicas[key];
        }

        public override void Collect(TComponent replica)
        {
            replicas.Remove(replica.Key);

            base.Collect(replica);
        }

        public void CollectAll()
        {
            foreach (var kvp in replicas.Values.ToArray())
            {
                kvp.gameObject.SetActive(false);
            }

            replicas.Clear();
        }

        public void ReleaseAll()
        {
            foreach (var replica in replicas.Values.ToArray())
            {
                replica.gameObject.Destroy();
            }

            replicas.Clear();
        }
    }

    [Serializable]

    public class ManagedObjectPool<TComponent> : ObjectPool<TComponent>

        where TComponent : Component
    {
        private readonly HashSet<TComponent> replicas = new HashSet<TComponent>();

        public override TComponent Generate()
        {
            var clone = base.Generate();

            replicas.Add(clone);

            return clone;
        }

        public override void Collect(TComponent replica)
        {
            replicas.Remove(replica);

            base.Collect(replica);
        }

        public void CollectAll()
        {
            foreach (var replica in replicas.ToArray())
            {
                replica.gameObject.SetActive(false);
            }

            replicas.Clear();
        }

        public void ReleaseAll()
        {
            foreach (var replica in replicas.ToArray())
            {
                replica.gameObject.Destroy();
            }

            replicas.Clear();
        }
    }
}