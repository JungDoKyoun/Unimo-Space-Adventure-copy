using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Pointed Random Spawner")]

    public sealed class PointedRandomSpawner : RandomSpawner
    {
        [Space]

        [SerializeField]

        private Transform[] spawnPoints = null;

        protected override void Spawn()
        {
            var pooledObject = Cloning();

            var position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

            var eulerAngles = new Vector3(0f, Random.Range(0, 360f), 0f);

            pooledObject.transform.SetPositionAndRotation(position, eulerAngles);

            pooledObject.SetActive(true);
        }
    }
}