using System;

using System.Reflection;

using UnityEngine;

namespace ZL.Unity
{
    [AttributeUsage(AttributeTargets.Class)]

    public sealed class CreateAfterSceneLoadAttribute : Attribute
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]

        private static void RuntimeInitialize()
        {
            foreach (var assemblies in AppDomain.CurrentDomain.GetAssemblies())
            {
                var types = assemblies.GetTypes();

                if (types == null)
                {
                    continue;
                }

                foreach (var type in types)
                {
                    if (type.GetCustomAttribute<CreateAfterSceneLoadAttribute>() == null)
                    {
                        continue;
                    }

                    var gameObject = new GameObject(type.Name.ToTitleCase(), type);

                    gameObject.DontDestroyOnLoad();
                }
            }
        }
    }
}