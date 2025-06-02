using UnityEngine;

using ZL.Unity.Debugging;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Pooling/Circular Random Spawner")]

    public sealed class CircularRandomSpawner : RandomSpawner
    {
        [Space]

        [SerializeField]

        private float radius = 0f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            GizmosEx.DrawPolygon(transform.position, radius, 64);
        }

        protected override void Spawn()
        {
            var pooledObject = Cloning();

            var point = Random.insideUnitCircle * radius;

            var position = transform.position + new Vector3(point.x, 0f, point.y);

            var eulerAngles = new Vector3(0f, Random.Range(0, 360f), 0f);

            pooledObject.transform.SetPositionAndRotation(position, eulerAngles);

            pooledObject.gameObject.SetActive(true);
        }
    }
}