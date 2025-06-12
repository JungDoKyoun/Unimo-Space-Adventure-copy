using UnityEngine;

using ZL.CS.Singleton;

using ZL.Unity.Tweening;

namespace ZL.Unity.UI
{
    public abstract class ScreenUI<TScreenUI> : ScreenUI, ISingleton<TScreenUI>

        where TScreenUI : ScreenUI<TScreenUI>
    {
        public static TScreenUI Instance
        {
            get => ISingleton<TScreenUI>.Instance;
        }

        protected virtual void Awake()
        {
            ISingleton<TScreenUI>.TrySetInstance((TScreenUI)this);
        }

        protected virtual void OnDestroy()
        {
            ISingleton<TScreenUI>.Release((TScreenUI)this);
        }
    }

    [AddComponentMenu("ZL/UI/Screen (UI)")]

    public class ScreenUI : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponentInParentOnly]

        private ScreenUIGroup screenGroup = null;

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [Essential]

        [PropertyField]

        [Margin]

        [ReadOnlyWhenEditMode]

        [Button(nameof(Appear))]

        [Button(nameof(Disappear))]

        private Fader fader = null;

        public Fader Fader
        {
            get => fader;
        }

        public virtual void Appear()
        {
            if (screenGroup != null)
            {
                screenGroup.SwapCurrent(this);
            }

            fader.FadeIn();
        }

        public virtual void Disappear()
        {
            fader.FadeOut();
        }
    }
}