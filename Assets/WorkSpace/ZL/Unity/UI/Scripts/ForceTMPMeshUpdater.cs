using System.Collections;

using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Force TMP Mesh Updater")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(TextMeshProUGUI))]

    public sealed class ForceTMPMeshUpdater : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(ForceMeshUpdate))]

        private TextMeshProUGUI text;

        private void OnEnable()
        {
            ForceMeshUpdate();
        }

        public void ForceMeshUpdate()
        {
            StartCoroutine(ForceMeshUpdateRoutine());
        }

        private IEnumerator ForceMeshUpdateRoutine()
        {
            yield return null;

            text.ForceMeshUpdate();
        }
    }
}