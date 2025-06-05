using JDG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JDG
{
    public static class EffectExecutor
    {
        private static Dictionary<EffectType, IEffect> _effectMap;

        static EffectExecutor()
        {
            _effectMap = new Dictionary<EffectType, IEffect>()
            {
                { EffectType.ChangeResource, new ChangeResource() },
                { EffectType.ChangeRelic, new ChangeRelic() },
                { EffectType.ChangeMaxHP, new ChangeMaxHP() },
                { EffectType.ChangeCurrentHP, new ChangeCurrentHP() },
                { EffectType.ChangeMaxFuel, new ChangeMaxFuel() },
                { EffectType.ChangeCurrentFuel, new ChangeCurrentFuel() }
            };

        }

        public static void ExecuteEffect(EventEffect eventEffect)
        {
            var effectType = eventEffect._effectType;

            if (_effectMap.TryGetValue(effectType, out IEffect effect))
            {
                effect.Execute(eventEffect);
            }
        }
    }
}
