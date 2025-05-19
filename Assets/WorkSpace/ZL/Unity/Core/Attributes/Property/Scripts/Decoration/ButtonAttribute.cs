using System.Reflection;

using Unity.VisualScripting;

namespace ZL.Unity
{
    public sealed class ButtonAttribute : CustomPropertyAttribute
    {
        private readonly string methodName;

        private readonly string text;

        public float Height { get; set; } = defaultLabelHeight;

        public BindingFlags Binding { get; set; } = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        public ButtonAttribute(string methodName, string text = null)
        {
            this.methodName = methodName;

            text ??= methodName?.SplitWords(' ');

            this.text = text;
        }

        #if UNITY_EDITOR

        private MethodInfo method = null;

        protected override void Initialize(Drawer drawer)
        {
            var type = drawer.TargetObject.GetType();

            method = type.GetMethod(methodName, Binding);
        }

        protected override void Draw(Drawer drawer)
        {
            drawer.DrawButton(method, text, Height);
        }

        #endif
    }
}