using System.Collections;

using UnityEngine;

using UnityEngine.Animations;

using ZL.Unity.Coroutines;

using ZL.Unity.Debugging;

using ZL.Unity.Pooling;

namespace ZL.Unity.Unimo
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField]

        [UsingCustomProperty]

        [Line]

        [Text("<b>�⺻ �ɼ�</b>", FontSize = 16)]

        [Margin]

        [Text("<b>���� �� ������Ʈ �̸�</b>")]

        private string spawnObjectName = "";

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���̺� Ƚ�� (0: ����)</b>")]

        private int waveCount = 0;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>ù ���̺� ���� (-1: ���� ����, 0: ��� ù ���̺�)</b>")]

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

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>������ ������Ʈ�� ���� (-1: ����)</b>")]

        protected float lifeTime = -1f;

        public float LifeTime
        {
            get => lifeTime;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>������Ʈ�� �����Ǵ� �Ÿ� (-1: ����)</b>")]

        protected float despawnDistance = -1f;

        public float DespawnDistance
        {
            get => despawnDistance;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>�÷��̾ ���� ȸ���ϴ� �ӵ� (-1: �⺻��)</b>")]

        private float rotationSpeed = -1f;

        public float RotationSpeed
        {
            get => rotationSpeed;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���� �� �ٶ� ���� (None: ���� ����)</b>")]

        private Transform lookPoint = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [ReadOnlyIf(nameof(lookPoint), null, true)]

        [Text("<b>���� �� Y�� ���� (-1: ���� ����)</b>")]

        private float lookAngle = -1f;

        protected int objectCount = 0;

        protected virtual void OnDrawGizmosSelected()
        {
            if (despawnDistance == -1f)
            {
                return;
            }

            Gizmos.color = Color.red;

            GizmosEx.DrawPolygon(transform.position, despawnDistance, 64);
        }

        private void OnEnable()
        {
            StartSpawning();
        }

        private void OnDisable()
        {
            spawningRoutine = null;
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
            gameObject.SetActive(false);
        }

        private IEnumerator spawningRoutine = null;

        private IEnumerator SpawningRoutine()
        {
            int waveCount = this.waveCount;

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

                yield return WaveRoutine();

                if (this.waveCount != 0 && --waveCount <= 0)
                {
                    break;
                }

                waveInterval = Random.Range(minWaveInterval, maxWaveInterval);
            }

            StopSpawning();
        }

        protected abstract IEnumerator WaveRoutine();

        protected void Spawn(Vector3 position)
        {
            ++objectCount;

            var spawnedObject = ObjectPoolManager.Instance.Clone<SpawnedObject>(spawnObjectName);

            Quaternion rotation;

            if (lookPoint != null)
            {
                if (QuaternionEx.TryLookRotation(position, lookPoint.position, Axis.Y, out rotation) == false)
                {
                    rotation = spawnedObject.transform.rotation;
                }
            }

            else if (lookAngle == -1)
            {
                rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            }

            else
            {
                rotation = Quaternion.Euler(0f, lookAngle, 0f);
            }

            spawnedObject.transform.SetPositionAndRotation(position, rotation);

            spawnedObject.Spawner = this;

            spawnedObject.Appear();
        }

        public void Despawn()
        {
            --objectCount;
        }
    }
}