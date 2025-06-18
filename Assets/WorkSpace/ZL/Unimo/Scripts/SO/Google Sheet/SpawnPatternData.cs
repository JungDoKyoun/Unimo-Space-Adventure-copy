using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Collections;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Spawn Pattern Data", fileName = "Spawn Pattern Data 1")]

    public sealed class SpawnPatternData : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private int[] spawnPoints = null;

        public int[] SpawnPoints
        {
            get => spawnPoints;
        }

        [Space]

        [SerializeField]

        private float interval = 0f;

        public override List<string> GetHeaders()
        {
            return new List<string>()
            {
                nameof(name),

                nameof(spawnPoints),
            };
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            spawnPoints = ArrayEx.Parse(sheet[name, nameof(spawnPoints)].value, ',', int.Parse);
        }

        public override List<string> Export()
        {
            return new List<string>()
            {
                name.ToString(),

                string.Join(", ", spawnPoints),
            };
        }
    }
}