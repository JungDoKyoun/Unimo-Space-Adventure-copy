using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Spawner Data", fileName = "Spawner Data 1")]

    public sealed class SpawnerData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private string spawnObject = "";

        public string SpawnObject
        {
            get => spawnObject;
        }

        [SerializeField]

        private float objectSpawnMinTime = 0f;

        public float ObjectSpawnMinTime
        {
            get => objectSpawnMinTime;
        }

        [SerializeField]

        private float objectSpawnMaxTime = 0f;

        public float ObjectSpawnMaxTime
        {
            get => objectSpawnMaxTime;
        }

        [SerializeField]

        private int objectSpawnMinCount = 0;

        public int ObjectSpawnMinCount
        {
            get => objectSpawnMinCount;
        }

        [SerializeField]

        private int objectSpawnMaxCount = 0;

        public int ObjectSpawnMaxCount
        {
            get => objectSpawnMaxCount;
        }

        [SerializeField]

        private int objectCountLimits = -1;

        public int ObjectCountLimits
        {
            get => objectCountLimits;
        }

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(spawnObject),

                nameof(objectSpawnMinTime),

                nameof(objectSpawnMaxTime),

                nameof(objectSpawnMinCount),

                nameof(objectSpawnMaxCount),

                nameof(objectCountLimits),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            spawnObject = sheet[name, nameof(spawnObject)].value;

            objectSpawnMinTime = float.Parse(sheet[name, nameof(objectSpawnMinTime)].value);

            objectSpawnMaxTime = float.Parse(sheet[name, nameof(objectSpawnMaxTime)].value);

            objectSpawnMinCount = int.Parse(sheet[name, nameof(objectSpawnMinCount)].value);

            objectSpawnMaxCount = int.Parse(sheet[name, nameof(objectSpawnMaxCount)].value);

            objectCountLimits = int.Parse(sheet[name, nameof(objectCountLimits)].value);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                spawnObject.ToString(),

                objectSpawnMinTime.ToString(),

                objectSpawnMaxTime.ToString(),

                objectSpawnMinCount.ToString(),

                objectSpawnMaxCount.ToString(),

                objectCountLimits.ToString(),
            };
        }
    }
}