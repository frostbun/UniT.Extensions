#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.CodeAnalysis;
    using UnityEngine;
    #if UNIT_ADDRESSABLES
    using UnityEngine.ResourceManagement.AsyncOperations;
    #endif

    public static class UnityExtensions
    {
        public static T Instantiate<T>(this T prefab) where T : Object
        {
            return Object.Instantiate(prefab);
        }

        public static GameObject Instantiate(this GameObject prefab, Vector3 position = default, Quaternion rotation = default, Transform? parent = null)
        {
            return Object.Instantiate(prefab, position, rotation, parent);
        }

        public static GameObject Instantiate(this GameObject prefab, Transform parent, bool instantiateInWorldSpace = false)
        {
            return Object.Instantiate(prefab, parent, instantiateInWorldSpace);
        }

        // ReSharper disable once MethodOverloadWithOptionalParameter
        public static T Instantiate<T>(this T prefab, Vector3 position = default, Quaternion rotation = default, Transform? parent = null) where T : Component
        {
            return Object.Instantiate(prefab, position, rotation, parent);
        }

        public static T Instantiate<T>(this T prefab, Transform parent, bool instantiateInWorldSpace = false) where T : Component
        {
            return Object.Instantiate(prefab, parent, instantiateInWorldSpace);
        }

        public static T DontDestroyOnLoad<T>(this T obj) where T : Object
        {
            Object.DontDestroyOnLoad(obj);
            return obj;
        }

        public static void Destroy(this Object obj)
        {
            Object.Destroy(obj);
        }

        public static void DestroyImmediate(this Object obj)
        {
            Object.DestroyImmediate(obj);
        }

        public static T? GetComponentOrDefault<T>(this GameObject gameObject)
        {
            return gameObject.TryGetComponent(out T result) ? result : default;
        }

        public static T? GetComponentOrDefault<T>(this Component component)
        {
            return component.TryGetComponent(out T result) ? result : default;
        }

        public static T GetComponentOrThrow<T>(this GameObject gameObject)
        {
            return gameObject.TryGetComponent(out T result) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {gameObject.name}");
        }

        public static T GetComponentOrThrow<T>(this Component component)
        {
            return component.TryGetComponent(out T result) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {component.name}");
        }

        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.TryGetComponent<T>(out _);
        }

        public static bool HasComponent<T>(this Component component)
        {
            return component.TryGetComponent<T>(out _);
        }

        public static T? GetComponentInChildrenOrDefault<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : default;
        }

        public static T? GetComponentInChildrenOrDefault<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : default;
        }

        public static T GetComponentInChildrenOrThrow<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {gameObject.name} children");
        }

        public static T GetComponentInChildrenOrThrow<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInChildren<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {component.name} children");
        }

        public static bool HasComponentInChildren<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInChildren<T>(out _, includeInactive);
        }

        public static bool HasComponentInChildren<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInChildren<T>(out _, includeInactive);
        }

        public static bool TryGetComponentInChildren<T>(this GameObject gameObject, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = gameObject.GetComponentInChildren<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        public static bool TryGetComponentInChildren<T>(this Component component, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = component.GetComponentInChildren<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        public static T? GetComponentInParentOrDefault<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInParent<T>(out var result, includeInactive) ? result : default;
        }

        public static T? GetComponentInParentOrDefault<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInParent<T>(out var result, includeInactive) ? result : default;
        }

        public static T GetComponentInParentOrThrow<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInParent<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {gameObject.name} parent");
        }

        public static T GetComponentInParentOrThrow<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInParent<T>(out var result, includeInactive) ? result : throw new MissingComponentException($"Component {typeof(T).Name} not found in {component.name} parent");
        }

        public static bool HasComponentInParent<T>(this GameObject gameObject, bool includeInactive = false)
        {
            return gameObject.TryGetComponentInParent<T>(out _, includeInactive);
        }

        public static bool HasComponentInParent<T>(this Component component, bool includeInactive = false)
        {
            return component.TryGetComponentInParent<T>(out _, includeInactive);
        }

        public static bool TryGetComponentInParent<T>(this GameObject gameObject, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = gameObject.GetComponentInParent<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        public static bool TryGetComponentInParent<T>(this Component component, [MaybeNullWhen(false)] out T result, bool includeInactive = false)
        {
            result = component.GetComponentInParent<T>(includeInactive);
            return result is { } && !result.Equals(null);
        }

        public static bool TryAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.HasComponent<T>()) return false;
            gameObject.AddComponent<T>();
            return true;
        }

        public static bool TryAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.TryAddComponent<T>();
        }

        public static T GetComponentOrAdd<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.TryGetComponent(out T result) ? result : gameObject.AddComponent<T>();
        }

        public static T GetComponentOrAdd<T>(this Component component) where T : Component
        {
            return component.gameObject.GetComponentOrAdd<T>();
        }

        public static string GetPathInHierarchy(this Transform transform)
        {
            return transform.parent is null
                ? transform.name
                : $"{transform.parent.GetPathInHierarchy()}/{transform.name}";
        }

        public static string GetPathInHierarchy(this GameObject gameObject)
        {
            return gameObject.transform.GetPathInHierarchy();
        }

        public static string GetPathInHierarchy(this Component component)
        {
            return component.transform.GetPathInHierarchy();
        }

        public static Sprite CreateSprite(this Texture2D texture, Vector2? pivot = null)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(.5f, .5f));
        }

        #if UNIT_ADDRESSABLES
        public static void WaitForResultOrThrow(this AsyncOperationHandle asyncOperation)
        {
            asyncOperation.WaitForCompletion();
            if (asyncOperation.Status is AsyncOperationStatus.Failed)
            {
                var exception = asyncOperation.OperationException;
                asyncOperation.Release();
                throw exception;
            }
        }

        public static T WaitForResultOrThrow<T>(this AsyncOperationHandle<T> asyncOperation)
        {
            asyncOperation.WaitForCompletion();
            if (asyncOperation.Status is AsyncOperationStatus.Failed)
            {
                var exception = asyncOperation.OperationException;
                asyncOperation.Release();
                throw exception;
            }
            return asyncOperation.Result;
        }
        #endif
    }
}