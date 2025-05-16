using System.Collections;

using UnityEngine;

using ZL.Unity.Coroutines;

using ZL.Unity.Singleton;

using ZL.Unity.Tweening;

namespace ZL.Unity.Directing
{
    [AddComponentMenu("ZL/Directing/Scene Director (Singleton)")]

    public class SceneDirector : SceneDirector<SceneDirector>
    {

    }

    [DefaultExecutionOrder((int)ScriptExecutionOrder.SceneDirector)]

    public abstract class SceneDirector<TSceneDirector> : MonoSingleton<TSceneDirector>

        where TSceneDirector : SceneDirector<TSceneDirector>
    {
        [Space]

        [SerializeField]

        protected float startDelay = 0f;

        [Space]

        [SerializeField]

        protected Fader screenFader = null;

        [SerializeField]

        protected float fadeDuration = 0f;

        protected virtual IEnumerator Start()
        {
            yield return WaitForSecondsCache.Get(startDelay);

            FadeIn();

            yield return WaitForSecondsCache.Get(fadeDuration);
        }

        public virtual void LoadScene(string sceneName)
        {
            FadeOut();
            
            FixedSceneManager.LoadScene(this, fadeDuration, sceneName);
        }

        public void FadeIn()
        {
            AudioListenerVolumeTweener.Instance?.Tween(1f, fadeDuration);

            screenFader?.FadeOut(fadeDuration);
        }

        public void FadeOut()
        {
            AudioListenerVolumeTweener.Instance?.Tween(0f, fadeDuration);

            screenFader?.FadeIn(fadeDuration);
        }

        public virtual void Quit()
        {
            FixedApplication.Quit();
        }
    }
}