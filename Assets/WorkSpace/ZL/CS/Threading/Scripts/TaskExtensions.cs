using System;

using System.Collections;

using System.Threading.Tasks;

namespace ZL.CS.Threading
{
    public static partial class TaskExtensions
    {
        public static IEnumerator WaitForCompleted<TTask>(this TTask instance, Action<TTask> callback = null)

            where TTask : Task
        {
            while (instance.IsCompleted == false)
            {
                yield return null;
            }

            callback?.Invoke(instance);
        }
    }
}