using UnityEngine;

namespace ZL.Unity.UI
{
    public abstract class TextController : MonoBehaviour
    {
        public abstract string Text { get; set; }

        public void SetText(int value)
        {
            Text = value.ToString();
        }

        public void SetText(float value)
        {
            Text = value.ToString();
        }
    }
}