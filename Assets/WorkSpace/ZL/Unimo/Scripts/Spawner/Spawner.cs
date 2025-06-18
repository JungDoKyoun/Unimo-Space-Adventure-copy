using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Coroutines;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class Spawner : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>스폰 시 바라볼 지점 (None: 지정 방향)</b>")]

        private Transform lookPoint = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyIf(nameof(lookPoint), null, true)]

        [Text("<b>스폰 시 Y축 방향 (-1: 랜덤 방향)</b>")]

        private float lookAngle = -1f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>스폰 할 오브젝트 이름</b>")]

        private string spawnObjectName = "";

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>첫 웨이브 간격 (-1: 기본값 사용, 0: 즉시 첫 웨이브)</b>")]

        private float waveInterval = -1f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>웨이브 최소 간격</b>")]

        private float minWaveInterval = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>웨이브 최대 간격</b>")]

        private float maxWaveInterval = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>스폰 사이의 딜레이</b>")]

        protected float spawnDelay = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>스폰된 오브젝트의 수명 (-1: 무한)</b>")]

        protected float lifeTime = -1f;

        protected int objectCount = 0;

        private void OnEnable()
        {
            StartSpawning();
        }

        private void OnDisable()
        {
            StopSpawning();
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
            float waveInterval = this.waveInterval;

            if (waveInterval == -1f)
            {
                waveInterval = Random.Range(minWaveInterval, maxWaveInterval);
            }

            while (true)
            {
                if (waveInterval != 0f)
                {
                    yield return WaitForSecondsCache.Get(waveInterval);
                }

                yield return SpawnRoutine();

                waveInterval = Random.Range(minWaveInterval, maxWaveInterval);
            }
        }

        protected abstract IEnumerator SpawnRoutine();

        protected void Spawn(Vector3 position)
        {
            ++objectCount;

            var clone = ObjectPoolManager.Instance.Clone(spawnObjectName);

            clone.transform.position = position;

            if (lookPoint != null)
            {
                clone.transform.LookAt(lookPoint.position, Axis.Y);
            }

            else if (lookAngle != -1)
            {
                clone.transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);
            }

            else
            {
                clone.transform.rotation = Quaternion.Euler(0f, 360f, 0f);
            }

            clone.LifeTime = lifeTime;

            clone.OnDisableAction += OnDespawn;

            clone.gameObject.SetActive(true);
        }

        private void OnDespawn()
        {
            --objectCount;
        }
    }
}