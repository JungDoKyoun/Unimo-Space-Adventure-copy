using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Pooling
{
    [AddComponentMenu("ZL/Pooling/Spawner")]

    public class Spawner : Spawner<Transform>
    {
        protected override Transform Cloning()
        {
            return ObjectPoolManager.Instance.Cloning(spawnerData.SpawnObject);
        }
    }

    public abstract class Spawner<TClone> : MonoBehaviour

        where TClone : Component
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        protected SpawnerData spawnerData = null;

        [Space]

        [SerializeField]

        private Transform[] spawnPoints = null;

        private int spawnCount = 0;

        private void Start()
        {
            StartSpawning();
        }

        public void StartSpawning()
        {
            if (spawnRoutine != null)
            {
                return;
            }

            spawnRoutine = SpawnRoutine();

            StartCoroutine(spawnRoutine);
        }

        public void StopSpawning()
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

        protected bool TryCloning(out TClone clone)
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

        protected abstract TClone Cloning();
    }
}