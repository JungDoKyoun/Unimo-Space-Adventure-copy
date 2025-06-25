using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

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

        [Text("<b>Rotation을 스폰 지점과 동기화</b>")]

        private bool syncRotation = false;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>스폰 지점 랜덤</b>")]

        [PropertyField]

        [Margin]

        [Text("<b>스폰 지점들 (배열 순서대로 스폰)</b>")]

        private bool randomSpawnPoint = false;

        [SerializeField]

        private Transform[] spawnPoints = null;

        private Transform[] spawnPointsClone = null;

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            Gizmos.color = new(0f, 1f, 0f, 0.5f);

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
                var spawnRotation = GetSpawnRotation(spawnPoints[i]);

                Spawn(spawnPoints[i].position, spawnRotation);

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

        private Quaternion GetSpawnRotation(Transform spawnPoint)
        {
            if (lookPoint != null)
            {
                if (QuaternionEx.TryLookRotation(spawnPoint.position, lookPoint.position, Axis.Y, out var spawnRotation) == true)
                {
                    return spawnRotation;
                }
            }

            if (syncRotation == false)
            {
                return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }

            return spawnPoint.rotation;
        }
    }
}