#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using UnityEngine;

    public class BetterMonoBehavior : MonoBehaviour
    {
        #region Self

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? GetComponentOrDefault<T>() => UnityExtensions.GetComponentOrDefault<T>(this);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new T GetComponent<T>() => UnityExtensions.GetComponentOrThrow<T>(this);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponent<T>() => UnityExtensions.HasComponent<T>(this);

        #endregion

        #region Children

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? GetComponentInChildrenOrDefault<T>(bool includeInactive = false) => UnityExtensions.GetComponentInChildrenOrDefault<T>(this, includeInactive);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new T GetComponentInChildren<T>(bool includeInactive = false) => UnityExtensions.GetComponentInChildrenOrThrow<T>(this, includeInactive);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponentInChildren<T>(bool includeInactive = false) => UnityExtensions.HasComponentInChildren<T>(this, includeInactive);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComponentInChildren<T>([MaybeNullWhen(false)] out T component, bool includeInactive = false) => UnityExtensions.TryGetComponentInChildren(this, out component, includeInactive);

        #endregion

        #region Parent

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T? GetComponentInParentOrDefault<T>(bool includeInactive = false) => UnityExtensions.GetComponentInParentOrDefault<T>(this, includeInactive);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public new T GetComponentInParent<T>(bool includeInactive = false) => UnityExtensions.GetComponentInParentOrThrow<T>(this, includeInactive);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasComponentInParent<T>(bool includeInactive = false) => UnityExtensions.HasComponentInParent<T>(this, includeInactive);

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetComponentInParent<T>([MaybeNullWhen(false)] out T component, bool includeInactive = false) => UnityExtensions.TryGetComponentInParent(this, out component, includeInactive);

        #endregion

        #region Async

        private CancellationTokenSource? disableCts;

        public CancellationToken GetCancellationTokenOnDisable()
        {
            return this.isActiveAndEnabled
                ? (this.disableCts ??= new()).Token
                : new(true);
        }

        protected virtual void OnDisable()
        {
            this.disableCts?.Cancel();
            this.disableCts?.Dispose();
            this.disableCts = null;
        }

        #endregion
    }
}