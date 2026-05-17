#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using MessagePipe;

    public static class MessagePipeExtensions
    {
        public static async UniTask WaitForSignalAsync<T>(this ISubscriber<T> subscriber, Func<T, bool>? filter = null, CancellationToken cancellationToken = default)
        {
            filter ??= static _ => true;
            var tcs = new UniTaskCompletionSource();
            using var _ = subscriber.Subscribe(signal =>
            {
                if (filter(signal)) tcs.TrySetResult();
            });
            await tcs.Task.AttachExternalCancellation(cancellationToken);
        }
    }
}