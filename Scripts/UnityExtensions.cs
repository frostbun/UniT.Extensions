namespace UniT.Extensions
{
    using UnityEngine;

    public static class UnityExtensions
    {
        public static T DontDestroyOnLoad<T>(this T obj) where T : Object
        {
            Object.DontDestroyOnLoad(obj);
            return obj;
        }

        public static string ToHex(this Color color)
        {
            return $"{(byte)(color.r * 255):X2}{(byte)(color.g * 255):X2}{(byte)(color.b * 255):X2}";
        }

        public static Sprite CreateSprite(this Texture2D texture, Vector2? pivot = null)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot ?? new Vector2(.5f, .5f));
        }
    }
}