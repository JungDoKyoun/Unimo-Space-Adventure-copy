using System.Collections;

using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Force TMP Mesh Updater")]

    public sealed class ForceTMPMeshUpdater : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [ReadOnly(true)]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(ForceMeshUpdate))]

        private TMP_Text text = null;

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