using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;
using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class RandomSpawner : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        protected SpawnerData spawnerData = null;

        private int spawnedCount = 0;

        private void Start()
        {
            StartSpawning();
        }

        public void StartSpawning()
        {
            if (spawningRoutine != null)
            {
                return;
            }

            spawningRoutine = SpawningRoutine();

            StartCoroutine(spawningRoutine);
        }

        public void StopSpawning()
        {
            if (spawningRoutine == null)
            {
                return;
            }

            StopCoroutine(spawningRoutine);

            spawningRoutine = null;
        }

        private IEnumerator spawningRoutine = null;

        private IEnumerator SpawningRoutine()
        {
            while (true)
            {
                float interval = Random.Range(spawnerData.ObjectSpawnMinTime, spawnerData.ObjectSpawnMaxTime);

                yield return WaitForSecondsCache.Get(interval);

                int spawnCount = Random.Range(spawnerData.ObjectSpawnMinCount, spawnerData.ObjectSpawnMaxCount);

                while (spawnCount-- > 0)
                {
                    if (spawnedCount >= spawnerData.ObjectCountLimits)
                    {
                        break;
                    }

                    Spawn();
                }
            }
        }

        protected abstract void Spawn();

        protected PooledObject Cloning()
        {
            ++spawnedCount;

            var pooledObject = ObjectPoolManager.Instance.Cloning(spawnerData.SpawnObject);

            pooledObject.OnDisableAction += OnDespawn;

            return pooledObject;
        }

        private void OnDespawn()
        {
            --spawnedCount;
        }
    }
}