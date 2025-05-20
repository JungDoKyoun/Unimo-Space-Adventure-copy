using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Spawner")]

    public sealed class Spawner : ObjectSpawner<Transform>
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private SpawnerData spawnerData = null;

        [Space]

        [SerializeField]

        private SpawnPatternData[] spawnPatternDatas = null;

        private int spawnCount = 0;

        private void Start()
        {
            StartRandomSpawning();
        }

        public void StartRandomSpawning()
        {
            if (spawnRoutine != null)
            {
                return;
            }

            spawnRoutine = SpawnRoutine();

            StartCoroutine(spawnRoutine);
        }

        public void StopRandomSpawning()
        {
            if (spawnRoutine == null)
            {
                return;
            }

            StopCoroutine(spawnRoutine);

            spawnRoutine = null;
        }

        private IEnumerator spawnRoutine = null;

        private IEnumerator SpawnRoutine()
        {
            while (true)
            {
                float interval = Random.Range(spawnerData.ObjectSpawnMinTime, spawnerData.ObjectSpawnMaxTime);

                yield return WaitForSecondsCache.Get(interval);

                int spawnCount = Random.Range(spawnerData.ObjectSpawnMinCount, spawnerData.ObjectSpawnMaxCount);

                while (spawnCount-- > 0)
                {
                    SpawnRandom();
                }
            }
        }

        protected override bool TryCloning(out Transform clone)
        {
            if (spawnCount >= spawnerData.ObjectCountLimits)
            {
                clone = null;

                return false;
            }

            ++spawnCount;

            clone = Cloning();

            return true;
        }

        protected override Transform Cloning()
        {
            return ObjectPoolManager.Instance.Cloning(spawnerData.SpawnObject);
        }
    }
}