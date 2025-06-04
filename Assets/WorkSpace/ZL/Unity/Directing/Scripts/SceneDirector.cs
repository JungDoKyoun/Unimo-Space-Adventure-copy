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

        protected AudioListenerVolumeTweener audioListenerVolumeTweener = null;

        protected override void Awake()
        {
            base.Awake();

            audioListenerVolumeTweener = AudioListenerVolumeTweener.Instance;
        }

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
            if (screenFader != null)
            {
                screenFader.FadeOut();
            }

            if (audioListenerVolumeTweener != null)
            {
                audioListenerVolumeTweener.SetEndValue(1f);

                audioListenerVolumeTweener.Play();
            }
        }

        public void FadeOut()
        {
            if (screenFader != null)
            {
                screenFader.FadeIn();
            }

            if (audioListenerVolumeTweener != null)
            {
                audioListenerVolumeTweener.SetEndValue(0f);

                audioListenerVolumeTweener.Play();
            }
        }

        public void Pause()
        {
            TimeEx.Pause();
        }

        public void Resume()
        {
            TimeEx.Resume();
        }

        public virtual void Quit()
        {
            FixedApplication.Quit();
        }
    }
}