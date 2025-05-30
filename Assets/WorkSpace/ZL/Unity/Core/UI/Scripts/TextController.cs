using UnityEngine;

namespace ZL.Unity.UI
{
    public abstract class TextController : MonoBehaviour
    {
        public abstract string text { get; set; }

        public void SetText(int value)
        {
            text = value.ToString();
        }

        public void SetText(float value)
        {
            text = value.ToString();
        }
    }
}