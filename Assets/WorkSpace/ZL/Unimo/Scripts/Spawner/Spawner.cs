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

        [Text("<b>���� �� �ٶ� ���� (null: ���� ����)</b>")]

        private Transform lookAt = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���� �� ������Ʈ �̸�</b>")]

        private string spawnObjectName = "";

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>ù ���̺� ���� (-1: ����, 0: ��� ù ���̺�)</b>")]

        private float waveInterval = -1f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���̺� �ּ� ����</b>")]

        private float minWaveInterval = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���̺� �ִ� ����</b>")]

        private float maxWaveInterval = 0f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���� ������ ������</b>")]

        protected float spawnDelay = 0f;

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

        protected PooledObject Clone()
        {
            ++objectCount;

            var clone = ObjectPoolManager.Instance.Clone(spawnObjectName);

            clone.OnDisableAction += OnDespawn;

            if (lookAt != null)
            {
                Debug.Log("���� ���� ��ġ��");

                transform.LookAt(lookAt.position, Axis.Y);
            }

            else
            {
                clone.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360f), 0f);
            }

            return clone;
        }

        private void OnDespawn()
        {
            --objectCount;
        }
    }
}