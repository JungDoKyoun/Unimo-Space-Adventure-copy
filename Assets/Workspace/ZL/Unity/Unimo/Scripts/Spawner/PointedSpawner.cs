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

        [Text("<b>스폰 지점들 (배열 순서대로 스폰)</b>")]

        private bool randomSpawnPoint = false;

        [SerializeField]

        private Transform[] spawnPoints = null;

        private Transform[] spawnPointsClone = null;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < spawnPoints.Length; ++i)
            {
                if (spawnPoints[i] != null)
                {
                    Gizmos.DrawSphere(spawnPoints[i].position, 0.5f);
                }
            }
        }

        private void OnValidate()
        {
            if (Application.isPlaying == true)
            {
                return;
            }

            spawnPointsClone = (Transform[])spawnPoints.Clone();
        }

        private void Awake()
        {
            spawnPointsClone = (Transform[])spawnPoints.Clone();

            spawnPointsClone.Shuffle();
        }

        protected override IEnumerator WaveRoutine()
        {
            if (this.spawnPoints.Length == 0)
            {
                yield break;
            }

            Transform[] spawnPoints;

            if (randomSpawnPoint == false)
            {
                spawnPoints = this.spawnPoints;
            }

            else
            {
                spawnPoints = spawnPointsClone;

                spawnPointsClone.Shuffle();
            }

            for (int i = 0; ; ++i)
            {
                Spawn(spawnPoints[i].position);

                if (i >= spawnPoints.Length - 1)
                {
                    yield break;
                }

                if (spawnDelay != 0f)
                {
                    yield return WaitForSecondsCache.Get(spawnDelay);
                }
            }
        }
    }
}