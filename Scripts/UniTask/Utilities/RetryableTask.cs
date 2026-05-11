#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine;

    public sealed class RetryableTask
    {
        private readonly int   retryCount;
        private readonly float retryIntervalSeconds;
        private readonly bool  doubleIntervalEachRetry;
        private readonly float maxRetryIntervalSeconds;

        public RetryableTask(int retryCount = -1, float retryIntervalSeconds = 1, bool doubleIntervalEachRetry = true, float maxRetryIntervalSeconds = 32)
        {
            this.retryCount              = retryCount;
            this.retryIntervalSeconds    = retryIntervalSeconds;
            this.doubleIntervalEachRetry = doubleIntervalEachRetry;
            this.maxRetryIntervalSeconds = maxRetryIntervalSeconds;
        }

        private CancellationTokenSource? cts;

        public async UniTask RunAsync<TState>(Func<TState, CancellationToken, UniTask<bool>> taskFactory, TState state)
        {
            this.cts ??= new();
            var ct      = this.cts.Token;
            var attempt = 0;
            while (true)
            {
                try
                {
                    if (await taskFactory(state, ct)) return;
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception)
                {
                    if (attempt == this.retryCount) throw;
                }
                ++attempt;
                var delaySeconds = Mathf.Min(
                    this.maxRetryIntervalSeconds,
                    this.retryIntervalSeconds * (this.doubleIntervalEachRetry ? Mathf.Pow(2, attempt - 1) : 1)
                );
                await UniTask.WaitForSeconds(delaySeconds, cancellationToken: ct);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask RunAsync(Func<CancellationToken, UniTask<bool>> taskFactory)
        {
            return this.RunAsync((taskFactory, ct) => taskFactory(ct), taskFactory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask RunAsync<TState>(Func<TState, CancellationToken, UniTask> taskFactory, TState state)
        {
            return this.RunAsync(async (state, ct) =>
            {
                await state.taskFactory(state.state, ct);
                return true;
            }, (taskFactory, state));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UniTask RunAsync(Func<CancellationToken, UniTask> taskFactory)
        {
            return this.RunAsync((taskFactory, ct) => taskFactory(ct), taskFactory);
        }

        public void Cancel()
        {
            this.cts?.Cancel();
            this.cts?.Dispose();
            this.cts = null;
        }
    }
}
#endif