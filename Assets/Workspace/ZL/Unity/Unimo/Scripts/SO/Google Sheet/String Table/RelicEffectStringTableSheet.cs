using System;

using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.SO.GoogleSheet;

namespace ZL.Unity.Unimo
{
    [CreateAssetMenu(menuName = "ZL/Unimo/SO/Relic String Table Sheet (Singleton)", fileName = "Relic String Table Sheet")]

    public sealed class RelicEffectStringTableSheet : ScriptableGoogleSheet<RelicEffectType, StringTable>, ISingleton<RelicEffectStringTableSheet>
    {
        public static RelicEffectStringTableSheet Instance
        {
            get => ISingleton<RelicEffectStringTableSheet>.Instance;
        }

        protected override RelicEffectType GetDataKey(StringTable data)
        {
            return Enum.Parse<RelicEffectType>(data.name);
        }
    }
}