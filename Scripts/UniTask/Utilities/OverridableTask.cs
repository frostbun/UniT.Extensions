#nullable enable
namespace UniT.Extensions.UniTask
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public sealed class OverridableTask
    {
        private CancellationTokenSource? cts;

        public async UniTask RunAsync<TState>(Func<TState, CancellationToken, UniTask> taskFactory, TState state)
        {
            this.Cancel();
            this.cts = new();
            await taskFactory(state, this.cts.Token);
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