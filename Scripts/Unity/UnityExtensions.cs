#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class UnityExtensions
    {
        public static T Instantiate<T>(this T prefab) where T : Object
        {
            return Object.Instantiate(prefab);
        }

        public static T Instantiate<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Object
        {
            return Object.Instantiate(prefab, position, rotation);
        }

        public static T Instantiate<T>(this T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : Object
        {
            return Object.Instantiate(prefab, position, rotation, parent);
        }

        public static T Instantiate<T>(this T prefab, Transform parent, bool worldPositionStays = false) where T : Object
        {
            return Object.Instantiate(prefab, parent, worldPositionStays);
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

        public static T GetComponentOrThrow<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() ?? throw new MissingComponentException($"Component {typeof(T).Name} not found in GameObject {gameObject.name}");
        }

        public static T GetComponentOrThrow<T>(this Component component)
        {
            return component.gameObject.GetComponentOrThrow<T>();
        }

        public static bool HasComponent<T>(this GameObject gameObject)
        {
            return gameObject.GetComponent<T>() is { };
        }

        public static bool HasComponent<T>(this Component component)
        {
            return component.gameObject.HasComponent<T>();
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

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>();
        }

        public static Sprite CreateSprite(this Texture2D texture, Vector2? pivot = null)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(.5f, .5f));
        }
    }
}