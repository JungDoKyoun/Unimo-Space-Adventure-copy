using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Debugging;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Object Spawner")]

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

        private float spawnRadius = 0f;

        [Space]

        [SerializeField]

        private Transform[] spawnPoints = null;

        private int spawnCount = 0;

        private int SpawnCountMax
        {
            get => spawnerData.ObjectCountLimits;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            GizmosEx.DrawPolygon(transform.position, spawnRadius, 64);
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

                    //SpawnRandomPoint();

                    SpawnRandomRange();
                }
            }
        }

        public void SpawnRandomRange()
        {
            var position = Random.insideUnitCircle * spawnRadius;

            var worldPosition = transform.position + new Vector3(position.x, 0f, position.y);

            Spawn(worldPosition, Quaternion.identity);
        }

        public void SpawnRandomPoint()
        {
            Spawn(Random.Range(0, spawnPoints.Length));
        }

        public void Spawn(int spawnPointIndex)
        {
            Spawn(spawnPoints[spawnPointIndex]);
        }

        private void Spawn(Transform spawnPoint)
        {
            Spawn(spawnPoint.position, spawnPoint.rotation);
        }

        private void Spawn(Vector3 position, Quaternion rotation)
        {
            ++spawnCount;

            var clone  = ObjectPoolManager.Instance.Cloning(spawnerData.SpawnObject);

            clone.OnDisableAction += OnDespawn;

            clone.transform.SetPositionAndRotation(position, rotation);

            clone.SetActive(true);
        }

        private void OnDespawn()
        {
            --spawnCount;
        }
    }
}