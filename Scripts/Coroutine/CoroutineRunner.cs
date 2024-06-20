#if !UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    #if !UNITY_WEBGL
    using System.Threading.Tasks;
    #endif

    public static class CoroutineRunner
    {
        private static readonly BetterMonoBehavior Runner = new GameObject(nameof(CoroutineRunner)).AddComponent<BetterMonoBehavior>().DontDestroyOnLoad();

        public static void Start(this IEnumerator coroutine) => Runner.StartCoroutine(coroutine);

        public static void Stop(this IEnumerator coroutine) => Runner.StopCoroutine(coroutine);

        public static IEnumerator Gather(this IEnumerable<IEnumerator> coroutines) => Runner.GatherCoroutines(coroutines);

        public static IEnumerator Gather(params IEnumerator[] coroutines) => Runner.GatherCoroutines(coroutines);

        public static IEnumerator Run(Action action, Action? callback = null)
        {
            #if !UNITY_WEBGL
            return Task.Run(action).ToCoroutine(callback);
            #else
            action();
            callback?.Invoke();
            yield break;
            #endif
        }

        public static IEnumerator Run<T>(Func<T> func, Action<T> callback)
        {
            #if !UNITY_WEBGL
            return Task.Run(func).ToCoroutine(callback);
            #else
            callback(func());
            yield break;
            #endif
        }
    }
}
#endif