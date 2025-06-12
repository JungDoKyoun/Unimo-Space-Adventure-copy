using UnityEngine;

namespace ZL.Unity.UI
{
    [AddComponentMenu("ZL/UI/Screen Group")]

    public sealed class ScreenUIGroup : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private ScreenUI main = null;

        [SerializeField]

        private ScreenUI current = null;

        [Space]

        [SerializeField]

        private bool disappearOnSwap = true;

        [SerializeField]

        private bool sortSibling = true;

        public void AppearMain()
        {
            if (main == null)
            {
                return;
            }

            main.Appear();
        }

        public void SwapCurrent(ScreenUI newScreen)
        {
            if (disappearOnSwap == true)
            {
                DisappearCurrent();
            }

            current = newScreen;

            if (sortSibling == true)
            {
                current.transform.SetAsLastSibling();
            }
        }

        public void DisappearCurrent()
        {
            if (current == null)
            {
                return;
            }

            current.Disappear();
        }
    }
}