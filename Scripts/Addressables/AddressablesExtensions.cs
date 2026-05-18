#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine.ResourceManagement.AsyncOperations;

    public static class AddressablesExtensions
    {
        public static async UniTask ToUniTask(this AsyncOperationHandle asyncOperation, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            try
            {
                while (!asyncOperation.IsDone)
                {
                    progress?.Report(asyncOperation.PercentComplete);
                    await UniTask.Yield(cancellationToken);
                }
                if (asyncOperation.IsValid() && asyncOperation.Status is AsyncOperationStatus.Failed)
                {
                    var exception = asyncOperation.OperationException;
                    asyncOperation.Release();
                    throw exception;
                }
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
                if (asyncOperation.IsValid() && asyncOperation.Status is AsyncOperationStatus.Failed)
                {
                    var exception = asyncOperation.OperationException;
                    asyncOperation.Release();
                    throw exception;
                }
                return asyncOperation.Result;
            }
            finally
            {
                if (!asyncOperation.IsDone) asyncOperation.Release();
            }
        }
    }
}