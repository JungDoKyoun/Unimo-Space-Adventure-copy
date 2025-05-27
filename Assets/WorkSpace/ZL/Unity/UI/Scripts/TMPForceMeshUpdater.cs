//using System.Collections;

using TMPro;

using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/TMP Force Mesh Updater")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.UIBuilder)]

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

        public void ForceMeshUpdate()
        {
            //StartCoroutine(ForceMeshUpdateRoutine());

            text.ForceMeshUpdate();
        }

        /*private IEnumerator ForceMeshUpdateRoutine()
        {
            yield return null;

            text.ForceMeshUpdate();
        }*/
    }
}