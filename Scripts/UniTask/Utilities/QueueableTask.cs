#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public sealed class QueueableTask
    {
        private readonly Queue<Func<CancellationToken, UniTask>> queue = new();

        private CancellationTokenSource? cts;
        private bool                     isRunning;

        public async UniTask RunAsync(Func<CancellationToken, UniTask> taskFactory)
        {
            this.cts ??= new();
            if (this.isRunning)
            {
                this.queue.Enqueue(taskFactory);
                while (this.isRunning || this.queue.Peek() != taskFactory)
                {
                    await UniTask.Yield(this.cts.Token);
                }
                this.queue.Dequeue();
            }
            this.isRunning = true;
            try
            {
                await taskFactory(this.cts.Token);
            }
            finally
            {
                this.isRunning = false;
            }
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
#endif