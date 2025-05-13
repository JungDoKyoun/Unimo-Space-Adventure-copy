using UnityEngine;

namespace ZL.Unity
{
    public sealed class EssentialAttribute : CustomPropertyAttribute
    {
        #if UNITY_EDITOR

        protected override void Draw(Drawer drawer)
        {
            if (drawer.fieldInfo.FieldType.IsSubclassOf(typeof(Object)) == false)
            {
                drawer.DrawErrorBox($"{NameTag} Field type is invalid.");

                return;
            }

            if (drawer.Property.objectReferenceValue == null)
            {
                drawer.DrawWarningBox($"{NameTag} This field must be assigned.");

                return;
            }
        }

        #endif
    }
}