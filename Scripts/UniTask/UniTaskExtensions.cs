#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine.Playables;
    #if UNIT_MESSAGEPIPE
    using MessagePipe;
    #endif

    public static class UniTaskExtensions
    {
        public static async UniTask PlayAsync(this PlayableDirector playableDirector, IProgress<float>? progress = null, CancellationToken cancellationToken = default)
        {
            playableDirector.Play();
            try
            {
                while (playableDirector.state is PlayState.Playing)
                {
                    progress?.Report((float)(playableDirector.time / playableDirector.duration));
                    await UniTask.Yield(cancellationToken);
                }
            }
            finally
            {
                playableDirector.Stop();
            }
        }

        #if UNIT_MESSAGEPIPE
        public static async UniTask WaitForSignalAsync<T>(this ISubscriber<T> subscriber, Func<T, bool>? filter = null, CancellationToken cancellationToken = default)
        {
            filter ??= _ => true;
            var tcs = new UniTaskCompletionSource();
            using var _ = subscriber.Subscribe(signal =>
            {
                if (filter(signal)) tcs.TrySetResult();
            });
            await tcs.Task.AttachExternalCancellation(cancellationToken);
        }
        #endif
    }
}
#endif