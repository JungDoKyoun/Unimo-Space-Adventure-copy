using System.Collections;

using UnityEngine;
using UnityEngine.Serialization;
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

        [Space]

        [SerializeField]

        //[UsingCustomProperty]

        //[Text("<b>������Ʈ�� �����Ǵ� �Ÿ� (-1: ����)</b>")]

        [FormerlySerializedAs("despawnDistance")]

        protected float despawnDistance = -1f;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>��ǥ�� ���� ȸ���ϴ� �ӵ� ���</b>")]

        private float rotationSpeedMultiplier = 1f;

        public float RotationSpeedMultiplier
        {
            get => rotationSpeedMultiplier;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>��ǥ�� ���� �ٰ����� �ӵ� ���</b>")]

        private float movementSpeedMultiplier = 1f;

        public float MovementSpeedMultiplier
        {
            get => movementSpeedMultiplier;
        }

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>���� �� �ٶ� ��ǥ (None: ���� ����)</b>")]

        protected Transform lookPoint = null;

        protected int objectCount = 0;

        protected virtual void OnDrawGizmosSelected()
        {
            if (despawnDistance == -1f)
            {
                return;
            }

            Gizmos.color = new(1f, 0f, 0f, 0.5f);

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
            Spawn(position, Quaternion.identity);
        }

        protected void Spawn(Vector3 position, Quaternion rotation)
        {
            ++objectCount;

            var spawnedObject = ObjectPoolManager.Instance.Clone<SpawnedObject>(spawnObjectName);

            spawnedObject.transform.SetPositionAndRotation(position, rotation);

            spawnedObject.OnDisappearedAction += Despawn;

            spawnedObject.LifeTime = lifeTime;

            spawnedObject.SpawnPosition = transform.position;

            spawnedObject.DespawnRange = despawnDistance;

            if (spawnedObject is Enemy enemy)
            {
                enemy.RotationSpeedMultiplier = rotationSpeedMultiplier;

                enemy.MovementSpeedMultiplier = movementSpeedMultiplier;
            }

            spawnedObject.Appear();
        }

        private void Despawn()
        {
            --objectCount;
        }
    }
}