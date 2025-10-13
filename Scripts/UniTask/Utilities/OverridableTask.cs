#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;

    public sealed class OverridableTask
    {
        private CancellationTokenSource? cts;

        public async UniTask RunAsync(Func<CancellationToken, UniTask> taskFactory)
        {
            this.cts?.Cancel();
            using var cts = this.cts = new();
            try
            {
                await taskFactory(cts.Token);
            }
            finally
            {
                if (this.cts == cts) this.cts = null;
            }
        }

        public void Cancel()
        {
            this.cts?.Cancel();
        }
    }
}
#endif