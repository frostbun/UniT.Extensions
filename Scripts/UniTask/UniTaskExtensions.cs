#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    #if UNIT_ADDRESSABLES
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine.ResourceManagement.AsyncOperations;
    #endif

    public static class UniTaskExtensions
    {
        #if UNIT_ADDRESSABLES
        public static async UniTask ToUniTask(this AsyncOperationHandle asyncOperation, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            await asyncOperation.ToUniTask(progress: progress, cancellationToken: cancellationToken, autoReleaseWhenCanceled: true);
            if (asyncOperation.Status is AsyncOperationStatus.Failed)
            {
                var exception = asyncOperation.OperationException;
                asyncOperation.Release();
                throw exception;
            }
        }

        public static async UniTask<T> ToUniTask<T>(this AsyncOperationHandle<T> asyncOperation, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            await asyncOperation.ToUniTask(progress: progress, cancellationToken: cancellationToken, autoReleaseWhenCanceled: true);
            if (asyncOperation.Status is AsyncOperationStatus.Failed)
            {
                var exception = asyncOperation.OperationException;
                asyncOperation.Release();
                throw exception;
            }
            return asyncOperation.Result;
        }
        #endif
    }
}
#endif