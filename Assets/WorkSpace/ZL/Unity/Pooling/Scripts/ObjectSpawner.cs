using UnityEngine;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Object Spawner")]

    public class ObjectSpawner : ObjectSpawner<Transform>
    {
        [Space]

        [SerializeField]

        private string key = "";

        protected override Transform Cloning()
        {
            return ObjectPoolManager.Instance.Cloning(key);
        }
    }

    public abstract class ObjectSpawner<TClone> : MonoBehaviour

        where TClone : Component
    {
        [Space]

        [SerializeField]

        private Transform[] spawnPoints = null;

        protected void Spawn()
        {
            if (TryCloning(out var clone) == false)
            {
                return;
            }

            clone.SetActive(true);
        }

        protected void Spawn(Vector3 position)
        {
            if (TryCloning(out var clone) == false)
            {
                return;
            }

            clone.transform.position = position;

            clone.SetActive(true);
        }

        protected void Spawn(Transform spawnPoint)
        {
            Spawn(spawnPoint.position);
        }

        protected void Spawn(int spawnPointIndex)
        {
            Spawn(spawnPoints[spawnPointIndex]);
        }

        protected void SpawnRandom()
        {
            Spawn(Random.Range(0, spawnPoints.Length));
        }

        protected virtual bool TryCloning(out TClone clone)
        {
            clone = Cloning();

            return true;
        }

        protected abstract TClone Cloning();
    }
}