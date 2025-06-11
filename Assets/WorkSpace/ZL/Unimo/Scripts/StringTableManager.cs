using UnityEngine;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/String Table Manager")]

    public sealed class StringTableManager : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private StringTableLanguage language = StringTableLanguage.English;

        private void OnValidate()
        {
            if (Application.isPlaying == false)
            {
                return;
            }

            StringTable.Language = language;
        }

        public void Start()
        {
            StringTable.Language = language;
        }
    }
}