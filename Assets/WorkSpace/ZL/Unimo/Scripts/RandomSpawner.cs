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

        [SerializeField]

        private float initialSpawnInterval = 0f;

        [Space]

        [SerializeField]

        private bool startSpawningOnEnable = true;

        private float spawnInterval = 0f;

        private int spawnedCount = 0;

        private void OnEnable()
        {
            if (startSpawningOnEnable == false)
            {
                return;
            }

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
            spawnInterval = initialSpawnInterval;

            if (spawnInterval == -1f)
            {
                spawnInterval = Random.Range(spawnerData.ObjectSpawnMinCount, spawnerData.ObjectSpawnMaxCount);
            }

            while (true)
            {
                if (spawnInterval != 0f)
                {
                    yield return WaitForSecondsCache.Get(spawnInterval);
                }

                int spawnCount = Random.Range(spawnerData.ObjectSpawnMinCount, spawnerData.ObjectSpawnMaxCount);

                while (spawnCount-- > 0)
                {
                    if (spawnedCount >= spawnerData.ObjectCountLimits)
                    {
                        break;
                    }

                    Spawn();
                }

                spawnInterval = Random.Range(spawnerData.ObjectSpawnMinTime, spawnerData.ObjectSpawnMaxTime);
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