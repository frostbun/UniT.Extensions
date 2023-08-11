namespace UniT.Extensions
{
    using System.Runtime.CompilerServices;
    using UnityEngine;

    public static class UnityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T DontDestroyOnLoad<T>(this T obj) where T : Object
        {
            Object.DontDestroyOnLoad(obj);
            return obj;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Sprite CreateSprite(this Texture2D texture, Vector2? pivot = null)
        {
            return Sprite.Create(texture, new(0, 0, texture.width, texture.height), pivot ?? new(0.5f, 0.5f));
        }
    }
}