using UnityEngine;

using ZL.Unity.Debugging;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Circular Random Spawner")]

    public sealed class CircularRandomSpawner : RandomSpawner
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Text("<b>��ȯ �ݰ� ������</b>")]

        private float radius = 0f;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            GizmosEx.DrawPolygon(transform.position, radius, 64);
        }

        protected override void Spawn()
        {
            var point = Random.insideUnitCircle * radius;

            Spawn(transform.position + new Vector3(point.x, 0f, point.y));
        }
    }
}