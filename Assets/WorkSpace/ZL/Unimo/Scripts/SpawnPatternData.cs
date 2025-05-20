using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Collections;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/Spawn Pattern Data", fileName = "Spawn Pattern Data")]

    public sealed class SpawnPatternData : ScriptableSheetData
    {
        [Space]

        [SerializeField]

        private float spawnDelay = 0f;

        public float SpawnDelay
        {
            get => spawnDelay;
        }

        [SerializeField]

        private bool isLoop = false;

        public bool IsLoop
        {
            get => isLoop;
        }


        [SerializeField]

        private int[] spawnPoints = null;

        public int[] SpawnPoints
        {
            get => spawnPoints;
        }

        public override List<string> GetHeader()
        {
            return new List<string>()
            {
                "name",

                nameof(spawnDelay),

                nameof(isLoop),

                nameof(spawnPoints),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            spawnDelay = float.Parse(sheet[name, nameof(spawnDelay)].value);

            isLoop = bool.Parse(sheet[name, nameof(isLoop)].value);

            spawnPoints = ArrayEx.Parse(sheet[name, nameof(spawnPoints)].value, int.Parse);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name,

                spawnDelay.ToString(),

                isLoop.ToString(),

                string.Join(", ", spawnPoints),
            };
        }
    }
}