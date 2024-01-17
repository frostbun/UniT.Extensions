namespace UniT.Extensions
{
    using UnityEngine;

    public static class UnityExtensions
    {
        public static T Instantiate<T>(this T prefab, Vector3 position = default, Quaternion rotation = default, Transform parent = null) where T : Object
        {
            return Object.Instantiate(prefab, position, rotation, parent);
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

        public static Sprite CreateSprite(this Texture2D texture, Vector2? pivot = null)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(.5f, .5f));
        }
    }
}