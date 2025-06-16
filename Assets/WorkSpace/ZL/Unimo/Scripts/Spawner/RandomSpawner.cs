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

        [Text("<b>���̺꿡 ��ȯ�� �ּ� ������Ʈ ��</b>")]

        protected int minSpawnCount = 0;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���̺꿡 ��ȯ�� �ִ� ������Ʈ ��</b>")]

        protected int maxSpawnCount = 0;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>��ȯ ������ ������Ʈ �� (-1: ������)</b>")]

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