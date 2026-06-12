#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public sealed class QueueableTask
    {
        private readonly Queue<object> queue = new();

        private CancellationTokenSource? cts;
        private bool                     isRunning;

        public async UniTask RunAsync<TState>(Func<TState, CancellationToken, UniTask> taskFactory, TState state) where TState : notnull
        {
            this.cts ??= new();
            if (this.isRunning)
            {
                var entry = new object();
                this.queue.Enqueue(entry);
                while (this.isRunning || this.queue.Peek() != entry)
                {
                    await UniTask.Yield(this.cts.Token);
                }
                this.queue.Dequeue();
            }
            this.isRunning = true;
            try
            {
                await taskFactory(state, this.cts.Token);
            }
            finally
            {
                this.isRunning = false;
            }
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
            this.queue.Clear();
        }
    }
}