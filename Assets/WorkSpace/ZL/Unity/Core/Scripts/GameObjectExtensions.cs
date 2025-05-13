using System;

using System.Collections.Generic;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class GameObjectExtensions
    {
        #region Get Component In Children

        public static bool TryGetComponentInChildren<TComponent>(this GameObject instance, out TComponent component)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInChildren(out component);
        }

        public static bool TryGetComponentInChildren(this GameObject instance, Type type, out Component component)
        {
            return instance.transform.TryGetComponentInChildren(type, out component);
        }

        public static bool TryGetComponentInChildrenOnly<TComponent>(this GameObject instance, out TComponent component)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInChildrenOnly(out component);
        }

        public static bool TryGetComponentInChildrenOnly(this GameObject instance, Type type, out Component component)
        {
            return instance.transform.TryGetComponentInChildrenOnly(type, out component);
        }

        public static bool TryGetComponentsInChildren<TComponent>(this GameObject instance, out List<TComponent> components)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInChildren(out components);
        }

        public static bool TryGetComponentsInChildrenOnly<TComponent>(this GameObject instance, out List<TComponent> components)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInChildrenOnly(out components);
        }

        #endregion

        #region GetComponentInParent

        public static bool TryGetComponentInParent<TComponent>(this GameObject instance, out TComponent component)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInParent(out component);
        }

        public static bool TryGetComponentInParent(this GameObject instance, Type type, out Component component)
        {
            return instance.transform.TryGetComponentInParent(type, out component);
        }

        public static bool TryGetComponentInParentOnly<TComponent>(this GameObject instance, out TComponent component)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentInParentOnly(out component);
        }

        public static bool TryGetComponentInParentOnly(this GameObject instance, Type type, out Component component)
        {
            return instance.transform.TryGetComponentInParentOnly(type, out component);
        }

        public static bool TryGetComponentsInParent<TComponent>(this GameObject instance, out List<TComponent> components)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInParent(out components);
        }

        public static bool TryGetComponentsInParentOnly<TComponent>(this GameObject instance, out List<TComponent> components)

            where TComponent : Component
        {
            return instance.transform.TryGetComponentsInParentOnly(out components);
        }

        #endregion

        public static GameObject AddComponents(this GameObject instance, params Type[] types)
        {
            foreach (var component in types)
            {
                instance.AddComponent(component);
            }

            return instance;
        }
    }
}