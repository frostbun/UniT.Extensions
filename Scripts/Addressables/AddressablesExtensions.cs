#nullable enable
namespace UniT.Extensions
{
    using System;
    using UnityEngine.ResourceManagement.AsyncOperations;
    #if UNIT_UNITASK
    using System.Threading;
    using Cysharp.Threading.Tasks;
    #else
    using System.Collections;
    #endif

    public static class AddressablesExtensions
    {
        public static void WaitForResultOrThrow(this AsyncOperationHandle asyncOperation)
        {
            try
            {
                #if UNITY_WEBGL
                if (!asyncOperation.IsDone) throw new InvalidOperationException("Cannot wait for async operation on WebGL");
                #endif
                asyncOperation.WaitForCompletion();
                asyncOperation.GetResultOrThrow();
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }

        public static T WaitForResultOrThrow<T>(this AsyncOperationHandle<T> asyncOperation)
        {
            try
            {
                #if UNITY_WEBGL
                if (!asyncOperation.IsDone) throw new InvalidOperationException("Cannot wait for async operation on WebGL");
                #endif
                asyncOperation.WaitForCompletion();
                return asyncOperation.GetResultOrThrow();
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }

        public static void GetResultOrThrow(this AsyncOperationHandle asyncOperation)
        {
            if (asyncOperation.IsValid() && asyncOperation.Status is AsyncOperationStatus.Failed)
            {
                var exception = asyncOperation.OperationException;
                asyncOperation.Release();
                throw exception;
            }
        }

        public static T GetResultOrThrow<T>(this AsyncOperationHandle<T> asyncOperation)
        {
            if (asyncOperation.IsValid() && asyncOperation.Status is AsyncOperationStatus.Failed)
            {
                var exception = asyncOperation.OperationException;
                asyncOperation.Release();
                throw exception;
            }
            return asyncOperation.Result;
        }

        #if UNIT_UNITASK
        public static async UniTask ToUniTask(this AsyncOperationHandle asyncOperation, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            try
            {
                while (!asyncOperation.IsDone)
                {
                    progress?.Report(asyncOperation.PercentComplete);
                    await UniTask.Yield(cancellationToken);
                }
                asyncOperation.GetResultOrThrow();
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }

        public static async UniTask<T> ToUniTask<T>(this AsyncOperationHandle<T> asyncOperation, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            try
            {
                while (!asyncOperation.IsDone)
                {
                    progress?.Report(asyncOperation.PercentComplete);
                    await UniTask.Yield(cancellationToken);
                }
                return asyncOperation.GetResultOrThrow();
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }
        #else
        public static IEnumerator ToCoroutine(this AsyncOperationHandle asyncOperation, Action? callback = null, IProgress<float>? progress = null)
        {
            try
            {
                while (!asyncOperation.IsDone)
                {
                    progress?.Report(asyncOperation.PercentComplete);
                    yield return null;
                }
                asyncOperation.GetResultOrThrow();
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
                callback(asyncOperation.GetResultOrThrow());
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }
        #endif
    }
}