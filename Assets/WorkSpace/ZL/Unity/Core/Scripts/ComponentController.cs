using UnityEngine;

namespace ZL.Unity
{
    [DisallowMultipleComponent]

    public abstract class ComponentController<TComponent> : MonoBehaviour

        where TComponent : Component
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        private TComponent target;

        public TComponent Target
        {
            get => target;
        }
    }
}