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
        #endif
    }
}
#endif