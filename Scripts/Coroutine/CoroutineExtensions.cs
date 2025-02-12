﻿#if !UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections;
    using System.Threading.Tasks;
    using UnityEngine;
    #if UNIT_ADDRESSABLES
    using UnityEngine.ResourceManagement.AsyncOperations;
    #endif

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
            while (true)
            {
                try
                {
                    if (!coroutine.MoveNext()) yield break;
                }
                catch (TException e)
                {
                    (coroutine as IDisposable)?.Dispose();
                    handler(e);
                    yield break;
                }
                yield return coroutine.Current;
            }
        }

        public static IEnumerator Catch<TException>(this IEnumerator coroutine, Action handler) where TException : Exception
        {
            return coroutine.Catch<TException>(_ => handler());
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
                while (coroutine.MoveNext()) yield return coroutine.Current;
            }
            finally
            {
                handler();
            }
        }

        public static IEnumerator ToCoroutine(this Task task, Action? callback = null)
        {
            task.ConfigureAwait(false);
            if (!task.IsCompleted) yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) throw task.Exception!;
            if (task.IsCanceled) yield break;
            callback?.Invoke();
        }

        public static IEnumerator ToCoroutine<T>(this Task<T> task, Action<T> callback)
        {
            task.ConfigureAwait(false);
            if (!task.IsCompleted) yield return new WaitUntil(() => task.IsCompleted);
            if (task.IsFaulted) throw task.Exception!;
            if (task.IsCanceled) yield break;
            callback(task.Result);
        }

        public static IEnumerator ToCoroutine(this AsyncOperation asyncOperation, Action? callback = null, IProgress<float>? progress = null)
        {
            while (!asyncOperation.isDone)
            {
                progress?.Report(asyncOperation.progress);
                yield return null;
            }
            callback?.Invoke();
        }

        #if UNIT_ADDRESSABLES
        public static IEnumerator ToCoroutine(this AsyncOperationHandle asyncOperation, Action? callback = null, IProgress<float>? progress = null)
        {
            try
            {
                while (!asyncOperation.IsDone)
                {
                    progress?.Report(asyncOperation.PercentComplete);
                    yield return null;
                }
                if (asyncOperation.Status is AsyncOperationStatus.Failed) throw asyncOperation.OperationException;
                callback?.Invoke();
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }

        public static IEnumerator ToCoroutine<T>(this AsyncOperationHandle<T> asyncOperation, Action<T> callback, IProgress<float>? progress = null)
        {
            try
            {
                while (!asyncOperation.IsDone)
                {
                    progress?.Report(asyncOperation.PercentComplete);
                    yield return null;
                }
                if (asyncOperation.Status is AsyncOperationStatus.Failed) throw asyncOperation.OperationException;
                callback(asyncOperation.Result);
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }
        #endif
    }
}
#endif