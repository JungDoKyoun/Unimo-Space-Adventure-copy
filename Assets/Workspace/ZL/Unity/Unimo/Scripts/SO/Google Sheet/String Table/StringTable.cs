using GoogleSheetsToUnity;

using System;

using System.Collections.Generic;

using UnityEngine;

using ZL.Unity.Collections;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/String Table", fileName = "String Table 1")]

    public sealed class StringTable : ScriptableGoogleSheetData
    {
        [Space]

        [SerializeField]

        private SerializableDictionary<StringTableLanguage, string> table = null;

        public string Value
        {
            get => this[StringTableManager.Instance.TargetLanguage];
        }

        public string this[StringTableLanguage key]
        {
            get => table[key];
        }

        public override List<string> GetHeaders()
        {
            var headers = new List<string>(table.Count + 1)
            {
                nameof(name)
            };

            foreach (var key in table.Keys)
            {
                headers.Add(key.ToString());
            }

            return headers;
        }

        public override void Import(GstuSpreadSheet sheet)
        {
            table = new SerializableDictionary<StringTableLanguage, string>();

            foreach (var key in sheet.columns.secondaryKeyLink.Keys)
            {
                if (Enum.TryParse<StringTableLanguage>(key, out var result) == false)
                {
                    continue;
                }

                table.Add(result, sheet[name, key].value);
            }

            table.Serialize();
        }

        public override List<string> Export()
        {
            var values = new List<string>(table.Count + 1)
            {
                name.ToString(),
            };

            foreach (var key in table.Values)
            {
                values.Add(key);
            }

            return values;
        }
    }
}