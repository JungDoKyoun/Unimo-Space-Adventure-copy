using System;

using UnityEngine;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]

    public sealed class EnumColorAttribute : EnumValueAttribute
    {
        public readonly Color value = default;

        public EnumColorAttribute(string hexColor)
        {
            if (ColorUtility.TryParseHtmlString(hexColor, out var value) == false)
            {
                FixedDebug.LogError("Hex Color is invalid.");

                return;
            }

            this.value = value;
        }
    }
}