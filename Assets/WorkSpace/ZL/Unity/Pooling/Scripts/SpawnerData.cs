using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Pooling
{
    [CreateAssetMenu(menuName = "ZL/Pooling/Spawner Data", fileName = "Spawner Data")]

    public class SpawnerData : ScriptableSheetData
    {
        [Space]

        [SerializeField]

        // �����ϴ� ������Ʈ ����
        private string spawnObject = "";

        public string SpawnObject
        {
            get => spawnObject;
        }

        [SerializeField]

        // ������Ʈ �����ֱ� �ּҰ�
        private float objectSpawnMinTime = 0f;

        public float ObjectSpawnMinTime
        {
            get => objectSpawnMinTime;
        }

        [SerializeField]

        // ������Ʈ �����ֱ��ִ�
        private float objectSpawnMaxTime = 0f;

        public float ObjectSpawnMaxTime
        {
            get => objectSpawnMaxTime;
        }

        [SerializeField]

        // ������Ʈ ���� ���� �ּҰ�
        private int objectSpawnMinCount = 0;

        public int ObjectSpawnMinCount
        {
            get => objectSpawnMinCount;
        }

        [SerializeField]

        // ������Ʈ ���� ���� �ִ�
        private int objectSpawnMaxCount = 0;

        public int ObjectSpawnMaxCount
        {
            get => objectSpawnMaxCount;
        }

        [SerializeField]

        // ������Ʈ ���� ���� ���� ����
        private int objectCountLimits = 0;

        public int ObjectCountLimits
        {
            get => objectCountLimits;
        }

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

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
                name,

                spawnObject,

                objectSpawnMinTime.ToString(),

                objectSpawnMaxTime.ToString(),

                objectSpawnMinCount.ToString(),

                objectSpawnMaxCount.ToString(),

                objectCountLimits.ToString(),
            };
        }
    }
}