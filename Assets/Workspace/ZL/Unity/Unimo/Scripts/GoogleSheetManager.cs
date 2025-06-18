using UnityEngine;

using ZL.CS.Singleton;

namespace ZL.Unity.Unimo
{
    [AddComponentMenu("ZL/Unimo/Google Sheet Manager")]

    [DefaultExecutionOrder((int)ScriptExecutionOrder.Singleton)]

    public sealed class GoogleSheetManager : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private RelicDataSheet relicDataSheet = null;

        [SerializeField]

        private RelicDropTableSheet relicDropTableSheet = null;

        [SerializeField]

        private RelicEffectStringTableSheet relicEffectStringTableSheet = null;

        private void Awake()
        {
            ISingleton<RelicDataSheet>.TrySetInstance(relicDataSheet);

            ISingleton<RelicDropTableSheet>.TrySetInstance(relicDropTableSheet);

            ISingleton<RelicEffectStringTableSheet>.TrySetInstance(relicEffectStringTableSheet);
        }

        private void OnDestroy()
        {
            ISingleton<RelicDataSheet>.Release(relicDataSheet);

            ISingleton<RelicDropTableSheet>.Release(relicDropTableSheet);

            ISingleton<RelicEffectStringTableSheet>.Release(relicEffectStringTableSheet);
        }
    }
}