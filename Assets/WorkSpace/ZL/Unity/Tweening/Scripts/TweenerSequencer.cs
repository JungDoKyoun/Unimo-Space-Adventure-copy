using UnityEngine;

using UnityEngine.Events;

namespace ZL.Unity.Tweening
{
    [AddComponentMenu("ZL/Tweenng/Tweener Sequencer")]

    public sealed class TweenerSequencer : MonoBehaviour
    {
        [Space]

        [SerializeField]

        private UnityEvent[] events;
    }
}