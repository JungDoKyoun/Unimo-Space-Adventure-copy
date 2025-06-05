using GoogleSheetsToUnity;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Collections;

using ZL.Unity.IO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/String Table", fileName = "String Table 1")]

    public sealed class StringTable : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<string, string> table = null;

        public override List<string> GetHeaders()
        {
            var headers = new List<string>(table.Count + 1)
            {
                nameof(name)
            };

            foreach (var key in table.Keys)
            {
                headers.Add(key);
            }

            return headers;
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            table = new SerializableDictionary<string, string>();

            foreach (var key in sheet.columns.secondaryKeyLink.Keys)
            {
                table.Add(key, sheet[name, key].value);
            }

            table.Remove(nameof(name));

            table.Serialize();
        }

        public override List<string> Export()
        {
            var values = new List<string>(table.Count + 1)
            {
                name
            };

            foreach (var key in table.Values)
            {
                values.Add(key);
            }

            return values;
        }
    }
}