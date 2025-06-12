using System.Collections;

using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/TMP Force Mesh Updater")]

    public sealed class TMPForceMeshUpdater : MonoBehaviour
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

        private void Start()
        {
            StartCoroutine(ForceMeshUpdateRoutine());
        }

        private IEnumerator ForceMeshUpdateRoutine()
        {
            yield return null;

            ForceMeshUpdate();

            //enabled = false;
        }

        public void ForceMeshUpdate()
        {
            text.ForceMeshUpdate(true, true);
        }
    }
}