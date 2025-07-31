#if UNIT_UNITASK
#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Threading;
    using Cysharp.Threading.Tasks;
    using UnityEngine.Playables;

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
    }
}
#endif