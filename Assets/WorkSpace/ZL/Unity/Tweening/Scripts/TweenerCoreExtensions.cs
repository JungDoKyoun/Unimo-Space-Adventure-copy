using DG.Tweening;

using DG.Tweening.Core;

using DG.Tweening.Plugins.Options;

namespace ZL.Unity.Tweening
{
    public static partial class TweenerCoreExtensions
    {
        public static float Remain<T1, T2, TPlugOptions>(this TweenerCore<T1, T2, TPlugOptions> instance)

            where TPlugOptions : struct, IPlugOptions
        {
            if (instance == null || instance.active == false)
            {
                return 0f;
            }

            return instance.Duration() - instance.Elapsed();
        }

        public static TweenerCore<T1, T2, QuaternionOptions> SetRotateMode<T1, T2>(this TweenerCore<T1, T2, QuaternionOptions> instance, RotateMode rotateMode)
        {
            instance.plugOptions.rotateMode = rotateMode;

            return instance;
        }
    }
}