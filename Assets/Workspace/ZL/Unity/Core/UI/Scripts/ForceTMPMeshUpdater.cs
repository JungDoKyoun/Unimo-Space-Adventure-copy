using System.Collections;

using TMPro;

#if UNITY_EDITOR

using UnityEditor;

#endif

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

        [Alias("Target Text (UI)")]

        [PropertyField]

        [ReadOnly(false)]

        [Button(nameof(ForceMeshUpdate))]

        private TMP_Text targetTextUI = null;

        private void Start()
        {
            StartCoroutine(ForceMeshUpdateRoutine());
        }

        private IEnumerator ForceMeshUpdateRoutine()
        {
            yield return null;

            ForceMeshUpdate();
        }

        public void ForceMeshUpdate()
        {
            targetTextUI.ForceMeshUpdate(true, true);
        }

        #if UNITY_EDITOR

        [MenuItem("Tools/ZL/Force TMP Mesh Update All")]

        #endif

        public static void ForceMeshUpdateAll()
        {
            foreach (var updater in FindObjectsOfType<ForceTMPMeshUpdater>(true))
            {
                updater.ForceMeshUpdate();

                FixedEditorUtility.SetDirty(updater);
            }

            FixedSceneView.RepaintAll();
        }
    }
}