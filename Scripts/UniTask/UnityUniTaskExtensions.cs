#nullable enable
namespace Cysharp.Threading.Tasks
{
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UniT.Extensions;
    using UnityEngine;

    public static class UnityUniTaskExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask OnDisableAsync(this GameObject gameObject)
        {
            return gameObject.GetComponentOrAdd<AsyncDisableTrigger>().OnDisableAsync();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask OnDisableAsync(this Component component)
        {
            return component.gameObject.OnDisableAsync();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationToken GetCancellationTokenOnDisable(this GameObject gameObject)
        {
            return gameObject.GetComponentOrAdd<AsyncDisableTrigger>().CancellationToken;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static CancellationToken GetCancellationTokenOnDisable(this Component component)
        {
            return component.gameObject.GetCancellationTokenOnDisable();
        }

        [DisallowMultipleComponent]
        private sealed class AsyncDisableTrigger : MonoBehaviour
        {
            private CancellationTokenSource? disableCts;

            public CancellationToken CancellationToken => this.isActiveAndEnabled
                ? (this.disableCts ??= new()).Token
                : new(true);

            public UniTask OnDisableAsync()
            {
                if (!this.isActiveAndEnabled) return UniTask.CompletedTask;
                var tcs = new UniTaskCompletionSource();
                this.CancellationToken.RegisterWithoutCaptureExecutionContext(tcs => ((UniTaskCompletionSource)tcs).TrySetResult(), tcs);
                return tcs.Task;
            }

            private void OnDisable()
            {
                this.disableCts?.Cancel();
                this.disableCts?.Dispose();
                this.disableCts = null;
            }
        }
    }
}