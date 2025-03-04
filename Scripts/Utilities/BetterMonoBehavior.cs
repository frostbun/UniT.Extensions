#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.CodeAnalysis;
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
        #region Self

        public T? GetComponentOrDefault<T>() => UnityExtensions.GetComponentOrDefault<T>(this);

        public new T GetComponent<T>() => UnityExtensions.GetComponentOrThrow<T>(this);

        public bool HasComponent<T>() => UnityExtensions.HasComponent<T>(this);

        #endregion

        #region Children

        public T? GetComponentInChildrenOrDefault<T>(bool includeInactive = false) => UnityExtensions.GetComponentInChildrenOrDefault<T>(this, includeInactive);

        public new T GetComponentInChildren<T>(bool includeInactive = false) => UnityExtensions.GetComponentInChildrenOrThrow<T>(this, includeInactive);

        public bool HasComponentInChildren<T>(bool includeInactive = false) => UnityExtensions.HasComponentInChildren<T>(this, includeInactive);

        public bool TryGetComponentInChildren<T>([MaybeNullWhen(false)] out T component, bool includeInactive = false) => UnityExtensions.TryGetComponentInChildren(this, out component, includeInactive);

        #endregion

        #region Parent

        public T? GetComponentInParentOrDefault<T>(bool includeInactive = false) => UnityExtensions.GetComponentInParentOrDefault<T>(this, includeInactive);

        public new T GetComponentInParent<T>(bool includeInactive = false) => UnityExtensions.GetComponentInParentOrThrow<T>(this, includeInactive);

        public bool HasComponentInParent<T>(bool includeInactive = false) => UnityExtensions.HasComponentInParent<T>(this, includeInactive);

        public bool TryGetComponentInParent<T>([MaybeNullWhen(false)] out T component, bool includeInactive = false) => UnityExtensions.TryGetComponentInParent(this, out component, includeInactive);

        #endregion

        #region Async

        #if UNIT_UNITASK
        private CancellationTokenSource? disableCts;

        public CancellationToken GetCancellationTokenOnDisable()
        {
            if (this.disableCts is { IsCancellationRequested: true } && this.isActiveAndEnabled)
            {
                this.disableCts.Dispose();
                this.disableCts = null;
            }
            return (this.disableCts ??= new CancellationTokenSource()).Token;
        }

        protected virtual void OnDisable()
        {
            this.disableCts?.Cancel();
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

        #endregion
    }
}