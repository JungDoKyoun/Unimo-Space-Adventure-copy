using System.Collections;

using UnityEngine;

using ZL.Unity.Collections;

using ZL.Unity.Coroutines;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Pointed Spawner")]

    public sealed class PointedSpawner : Spawner
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>랜덤 스폰 여부</b>")]

        [PropertyField]

        [Margin]

        [Text("<b>스폰 지점들</b>")]

        private bool randomSpawnPoint = false;

        [SerializeField]

        private Transform[] spawnPoints = null;

        private Transform[] spawnPointsClone = null;

        private void Awake()
        {
            spawnPointsClone = (Transform[])spawnPoints.Clone();
        }

        protected override IEnumerator SpawnRoutine()
        {
            Transform[] spawnPoints;

            if (randomSpawnPoint == false)
            {
                spawnPoints = this.spawnPoints;
            }

            else
            {
                spawnPointsClone.Shuffle();

                spawnPoints = spawnPointsClone;
            }

            for (int i = 0; ; ++i)
            {
                var clone = Clone();

                clone.transform.position = spawnPoints[i].position;

                clone.gameObject.SetActive(true);

                if (i == spawnPoints.Length - 1)
                {
                    break;
                }

                if (spawnDelay != 0f)
                {
                    yield return WaitForSecondsCache.Get(spawnDelay);
                }
            }
        }
    }
}