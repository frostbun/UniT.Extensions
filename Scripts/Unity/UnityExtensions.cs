#nullable enable
namespace UniT.Extensions
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Diagnostics.Contracts;
    using System.Runtime.CompilerServices;
    using UnityEngine;
    using Object = UnityEngine.Object;
    #if UNIT_ZSTRING
    using Cysharp.Text;
    #endif

    public static class UnityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this T prefab) where T : Object
        {
            return Object.Instantiate(prefab);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject Instantiate(this GameObject prefab, Vector3? position = null, Quaternion? rotation = null, Transform? parent = null)
        {
            return Object.Instantiate(prefab, position ?? Vector3.zero, rotation ?? Quaternion.identity, parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameObject Instantiate(this GameObject prefab, Transform parent, bool instantiateInWorldSpace = false)
        {
            return Object.Instantiate(prefab, parent, instantiateInWorldSpace);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this T prefab, Vector3? position = null, Quaternion? rotation = null, Transform? parent = null) where T : Component
        {
            return Object.Instantiate(prefab, position ?? Vector3.zero, rotation ?? Quaternion.identity, parent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this T prefab, Transform parent, bool instantiateInWorldSpace = false) where T : Component
        {
            return Object.Instantiate(prefab, parent, instantiateInWorldSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DontDestroyOnLoad<T>(this T obj) where T : Object
        {
            Object.DontDestroyOnLoad(obj);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(this Object obj)
        {
            Object.Destroy(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyImmediate(this Object obj)
        {
            Object.DestroyImmediate(obj);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetComponentOrDefault<T>(this GameObject gameObject)
        {
            return gameObject.TryGetComponent(out T result) ? result : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetComponentOrDefault<T>(this Component component)
        {
            return component.TryGetComponent(out T result) ? result : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentOrThrow<T>(this GameObject gameObject)
        {
            return gameObject.TryGetComponent(out T result) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {gameObject.name}");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentOrThrow<T>(this Component component)
        {
            return component.TryGetComponent(out T result) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {component.name}");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.TryGetComponent<T>(out _);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponent<T>(this Component component)
        {
            return component.TryGetComponent<T>(out _);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetComponentInChildrenOrDefault<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetComponentInChildrenOrDefault<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentInChildrenOrThrow<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {gameObject.name} children");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentInChildrenOrThrow<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {component.name} children");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInChildren<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInChildren<T>(out _, includeInactive);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInChildren<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInChildren<T>(out _, includeInactive);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = gameObject.GetComponentInChildren<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInChildren<T>(this Component component, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = component.GetComponentInChildren<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetComponentInParentOrDefault<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInParent<T>(out var result, includeInactive) ? result : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? GetComponentInParentOrDefault<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInParent<T>(out var result, includeInactive) ? result : default;
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentInParentOrThrow<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInParent<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {gameObject.name} parent");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentInParentOrThrow<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInParent<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {component.name} parent");
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInParent<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInParent<T>(out _, includeInactive);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasComponentInParent<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInParent<T>(out _, includeInactive);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParent<T>(this GameObject gameObject, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = gameObject.GetComponentInParent<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetComponentInParent<T>(this Component component, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = component.GetComponentInParent<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.HasComponent<T>()) return false;
            gameObject.AddComponent<T>();
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.TryAddComponent<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentOrAdd<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T result) ? result : gameObject.AddComponent<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetComponentOrAdd<T>(this Component component) where T : Component
        {
            return component.gameObject.GetComponentOrAdd<T>();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T? NullIfDestroyed<T>(this T? obj) where T : Object
        {
            return obj ? obj : null;
        }

        [Pure]
        public static string GetPathInHierarchy(this Transform transform)
        {
            var stack = new Stack<string>();
            while (transform != null)
            {
                stack.Push(transform.name);
                transform = transform.parent;
            }
            #if UNIT_ZSTRING
            return ZString.Join("/", stack);
            #else
            return string.Join("/", stack);
            #endif
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetPathInHierarchy(this GameObject gameObject)
        {
            return gameObject.transform.GetPathInHierarchy();
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetPathInHierarchy(this Component component)
        {
            return component.transform.GetPathInHierarchy();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLayer(this Transform transform, int layer)
        {
            transform.gameObject.layer = layer;
            var childCount = transform.childCount;
            for (var i = 0; i < childCount; ++i)
            {
                transform.GetChild(i).SetLayer(layer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLayer(this GameObject gameObject, int layer)
        {
            gameObject.transform.SetLayer(layer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetLayer(this Component component, int layer)
        {
            component.transform.SetLayer(layer);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sprite CreateSprite(this Texture2D texture, Rect? rect = null, Vector2? pivot = null)
        {
            return Sprite.Create(texture, rect ?? new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(.5f, .5f));
        }
    }
}