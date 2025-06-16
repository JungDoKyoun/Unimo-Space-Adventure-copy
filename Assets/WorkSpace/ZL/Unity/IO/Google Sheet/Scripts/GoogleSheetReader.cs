using UnityEngine;

namespace ZL.Unity.IO.GoogleSheet
{
    [AddComponentMenu("ZL/Unimo/Google Sheet Reader")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.Loader)]

    public sealed class GoogleSheetReader : MonoBehaviour
    {
        #if UNITY_EDITOR

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Button(nameof(ReadAllSheets))]

        [Margin]

        private bool readAllSheetsOnAwake = true;

        #endif

        [Space]

        [SerializeField]

        private ScriptableGoogleSheet[] sheets = null;

        private void Awake()
        {
            #if UNITY_EDITOR

            if (readAllSheetsOnAwake == false)
            {
                return;
            }

            #endif

            ReadAllSheets();
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