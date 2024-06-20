#if !UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class CoroutineExtensions
    {
        public static IEnumerator Then(this IEnumerator coroutine, Action callback)
        {
            yield return coroutine;
            callback();
        }

        public static IEnumerator Then(this YieldInstruction coroutine, Action callback)
        {
            yield return coroutine;
            callback();
        }

        public static IEnumerator Then(this IEnumerator coroutine, Func<IEnumerator> callback)
        {
            yield return coroutine;
            yield return callback();
        }

        public static IEnumerator Then(this YieldInstruction coroutine, Func<IEnumerator> callback)
        {
            yield return coroutine;
            yield return callback();
        }

        public static IEnumerator Then(this IEnumerator coroutine, Func<YieldInstruction> callback)
        {
            yield return coroutine;
            yield return callback();
        }

        public static IEnumerator Then(this YieldInstruction coroutine, Func<YieldInstruction> callback)
        {
            yield return coroutine;
            yield return callback();
        }

        public static IEnumerator Then(this IEnumerator coroutine, IEnumerator callback)
        {
            yield return coroutine;
            yield return callback;
        }

        public static IEnumerator Then(this YieldInstruction coroutine, IEnumerator callback)
        {
            yield return coroutine;
            yield return callback;
        }

        public static IEnumerator Then(this IEnumerator coroutine, YieldInstruction callback)
        {
            yield return coroutine;
            yield return callback;
        }

        public static IEnumerator Then(this YieldInstruction coroutine, YieldInstruction callback)
        {
            yield return coroutine;
            yield return callback;
        }

        public static IEnumerator Catch<TException>(this IEnumerator coroutine, Action<TException> handler) where TException : Exception
        {
            try
            {
                while (true)
                {
                    try
                    {
                        if (!coroutine.MoveNext()) break;
                    }
                    catch (TException e)
                    {
                        handler(e);
                        yield break;
                    }
                    yield return coroutine.Current;
                }
            }
            finally
            {
                (coroutine as IDisposable)?.Dispose();
            }
        }

        public static IEnumerator Catch(this IEnumerator coroutine, Action<Exception> handler)
        {
            return coroutine.Catch<Exception>(handler);
        }

        public static IEnumerator Catch(this IEnumerator coroutine, Action handler)
        {
            return coroutine.Catch(_ => handler());
        }

        public static IEnumerator Finally(this IEnumerator coroutine, Action handler)
        {
            try
            {
                yield return coroutine;
            }
            finally
            {
                handler();
            }
        }

        public static void Wait(this IEnumerator coroutine)
        {
            while (coroutine.MoveNext()) ;
        }

        public static IEnumerator ToCoroutine(this Task task, Action? callback = null)
        {
            task.ConfigureAwait(false);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) throw task.Exception!;
            if (task.IsCanceled) yield break;
            callback?.Invoke();
        }

        public static IEnumerator ToCoroutine<T>(this Task<T> task, Action<T> callback)
        {
            task.ConfigureAwait(false);
            yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) throw task.Exception!;
            if (task.IsCanceled) yield break;
            callback(task.Result);
        }
    }
}
#endif