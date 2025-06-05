using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.CS.Collections;

using ZL.Unity.IO.GoogleSheet;

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
                name,

                string.Join(", ", spawnPoints),
            };
        }
    }
}