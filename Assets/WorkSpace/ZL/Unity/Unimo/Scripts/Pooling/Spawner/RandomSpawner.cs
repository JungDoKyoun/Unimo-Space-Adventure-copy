using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Unimo
{
    public abstract class RandomSpawner : Spawner
    {
        [SerializeField]

        [UsingCustomProperty]

        [Line]

        [Text("<b>랜덤 스포너 옵션</b>", FontSize = 16)]

        [Margin]

        [Text("<b>스폰 시 Y축 방향 (-1: 랜덤 방향)</b>")]

        private float lookAngle = -1f;

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

        protected Quaternion GetSpawnRotation(Vector3 spawnPosition)
        {
            if (lookPoint != null)
            {
                if (QuaternionEx.TryLookRotation(spawnPosition, lookPoint.position, Axis.Y, out var rotation) == true)
                {
                    return rotation;
                }
            }

            if (lookAngle == -1f)
            {
                return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }

            return Quaternion.Euler(0f, lookAngle, 0f);
        }
    }
}