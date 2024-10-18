#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;
    #if UNIT_UNITASK
    using System.Threading;
    #else
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    #endif

    public class BetterMonoBehavior : MonoBehaviour
    {
        #if UNIT_UNITASK
        private CancellationTokenSource? disableCts;

        public CancellationToken GetCancellationTokenOnDisable()
        {
            return (this.disableCts ??= new CancellationTokenSource()).Token;
        }

        protected virtual void OnDisable()
        {
            this.disableCts?.Cancel();
            this.disableCts?.Dispose();
            this.disableCts = null;
        }
        #else
        private readonly HashSet<IEnumerator> runningCoroutines = new HashSet<IEnumerator>();

        public new void StartCoroutine(IEnumerator coroutine)
        {
            if (!this.runningCoroutines.Add(coroutine)) throw new InvalidOperationException("Coroutine is already running");
            base.StartCoroutine(coroutine.Finally(() => this.runningCoroutines.Remove(coroutine)));
        }

        public new void StopCoroutine(IEnumerator coroutine)
        {
            if (!this.runningCoroutines.Remove(coroutine)) throw new InvalidOperationException("Coroutine is not running");
            base.StopCoroutine(coroutine);
            (coroutine as IDisposable)?.Dispose();
        }

        public IEnumerator GatherCoroutines(params IEnumerator[] coroutines)
        {
            var count     = coroutines.Length;
            var exception = default(Exception);
            coroutines.ForEach(coroutine => this.StartCoroutine(coroutine.Catch(e => exception = exception is null ? e : throw e).Finally(() => --count)));
            while (count > 0)
            {
                if (exception is { }) throw exception;
                yield return null;
            }
            if (exception is { }) throw exception;
        }

        public IEnumerator GatherCoroutines(IEnumerable<IEnumerator> coroutines)
        {
            return this.GatherCoroutines(coroutines.ToArray());
        }

        protected virtual void OnDisable()
        {
            this.runningCoroutines.SafeForEach(this.StopCoroutine);
        }
        #endif
    }
}