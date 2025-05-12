using System;

using UnityEngine;

namespace ZL.Unity
{
    public static partial class ObjectCreator
    {
        public static TComponent CreateComponent<TComponent>(Transform parent, params Type[] components)

            where TComponent : Component
        {
            var name = typeof(TComponent).Name.ToTitleCase();

            return CreateComponent<TComponent>(name, parent, components);
        }

        public static TComponent CreateComponent<TComponent>(string name, Transform parent, params Type[] components)

            where TComponent : Component
        {
            var component = CreateComponent<TComponent>(name, parent);

            component.AddComponents(components);

            return component;
        }

        public static TComponent CreateComponent<TComponent>(string name, Transform parent)
            
            where TComponent : Component
        {
            var gameObject = CreateGameObject(name, parent);

            return gameObject.AddComponent<TComponent>();
        }

        public static GameObject CreateGameObject(string name, Transform parent, params Type[] components)
        {
            var gameObject = CreateGameObject(name, components);

            gameObject.transform.SetParent(parent, false);

            return gameObject;
        }

        public static GameObject CreateGameObject(string name, params Type[] components)
        {
            var gameObject = new GameObject(name, components);

            FixedUndo.RegisterCreatedObjectUndo(gameObject, $"Create {gameObject.name}");

            return gameObject;
        }
    }
}