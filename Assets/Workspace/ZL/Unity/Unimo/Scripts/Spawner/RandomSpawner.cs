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

        protected override IEnumerator WaveRoutine()
        {
            int spawnCount = Random.Range(minSpawnCount, maxSpawnCount);

            bool IsSpawnable()
            {
                if (maxObjectCount != -1 && objectCount >= maxObjectCount)
                {
                    return false;
                }

                if (spawnCount == 0)
                {
                    return false;
                }

                return true;
            }

            if (IsSpawnable() == false)
            {
                yield break;
            }

            while (true)
            {
                Spawn();

                --spawnCount;

                if (IsSpawnable() == false)
                {
                    yield break;
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