using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    [AddComponentMenu("ZL/Unimo/Google Sheet Reader")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.Loader)]

    public sealed class GoogleSheetReader : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [PropertyField]

        [Margin]

        [Button(nameof(ReadAllSheets))]

        private bool readAllSheetsOnAwake = true;

        [Space]

        [SerializeField]

        private ScriptableGoogleSheet[] sheets = null;

        private void Awake()
        {
            if (readAllSheetsOnAwake == true)
            {
                ReadAllSheets();
            }
        }

        public void ReadAllSheets()
        {
            for (int i = 0; i < sheets.Length; ++i)
            {
                sheets[i].Read();
            }
        }
    }
}