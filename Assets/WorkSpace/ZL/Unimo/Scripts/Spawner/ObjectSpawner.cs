using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Object Spawner")]

    public sealed class ObjectSpawner : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Essential]

        [ReadOnlyWhenPlayMode]

        private SpawnerData spawnerData = null;

        [SerializeField]

        private SpawnPatternData spawnPatternData = null;

        [Space]

        [SerializeField]

        private Transform[] spawnPoints = null;

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

                foreach (int spawnPointIndex in spawnPatternData.SpawnPoints)
                {
                    var pooledObject = ObjectPoolManager.Instance.Cloning(spawnerData.SpawnObject);

                    var position = spawnPoints[spawnPointIndex].position;

                    pooledObject.transform.position = position;

                    pooledObject.gameObject.SetActive(true);
                }
            }
        }
    }
}