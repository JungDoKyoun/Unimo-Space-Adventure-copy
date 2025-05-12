using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("")]

    [DisallowMultipleComponent]

    public sealed class TextController : MonoBehaviour
    {
        private ITextController target;

        public ITextController Target
        {
            get
            {
                #if UNITY_EDITOR

                target ??= GetComponent<ITextController>();

                #endif

                return target;
            }
        }

        public string Text
        {
            get => Target.Text;

            set => Target.Text = value;
        }

        private void Awake()
        {
            target = GetComponent<ITextController>();
        }

        public void SetText(int value)
        {
            Target.SetText(value);
        }

        public void SetText(float value)
        {
            Target.SetText(value);
        }
    }
}