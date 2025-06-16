using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Unimo
{
    public abstract class RandomSpawner : Spawner
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>웨이브에 소환할 최소 오브젝트 수</b>")]

        protected int minSpawnCount = 0;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>웨이브에 소환할 최대 오브젝트 수</b>")]

        protected int maxSpawnCount = 0;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>소환 가능한 오브젝트 수 (-1: 무제한)</b>")]

        protected int maxObjectCount = -1;

        protected override IEnumerator SpawnRoutine()
        {
            if (objectCount >= maxObjectCount)
            {
                yield break;
            }

            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

            if (spawnCount == 0)
            {
                yield break;
            }

            while (true)
            {
                Spawn();

                if (objectCount >= maxObjectCount)
                {
                    break;
                }

                if (--spawnCount == 0)
                {
                    break;
                }

                if (spawnDelay != 0f)
                {
                    yield return WaitForSecondsCache.Get(spawnDelay);
                }
            }
        }

        protected abstract void Spawn();
    }
}