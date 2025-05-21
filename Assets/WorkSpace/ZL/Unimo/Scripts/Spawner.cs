using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Spawner")]

    public sealed class Spawner : MonoBehaviour
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

        [Space]

        [SerializeField]

        private Transform[] spawnPoints = null;

        [Space]

        [SerializeField]

        private int spawnCount = 0;

        private int SpawnCountMax
        {
            get => spawnerData.ObjectCountLimits;
        }

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
                    if (SpawnCountMax != -1 && this.spawnCount >= SpawnCountMax)
                    {
                        break;
                    }

                    SpawnRandom();
                }
            }
        }

        public void SpawnRandom()
        {
            Spawn(Random.Range(0, spawnPoints.Length));
        }

        public void Spawn(int spawnPointIndex)
        {
            Spawn(spawnPoints[spawnPointIndex]);
        }

        public void Spawn(Transform spawnPoint)
        {
            ++spawnCount;

            var clone = ObjectPoolManager.Instance.Cloning(spawnerData.SpawnObject);

            clone.OnDisableAction += OnDespawn;

            clone.transform.SetPositionAndRotation(spawnPoint);

            clone.SetActive(true);
        }

        private void OnDespawn()
        {
            --spawnCount;
        }
    }
}