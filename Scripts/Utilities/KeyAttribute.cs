#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class KeyAttribute : Attribute
    {
        public string Key { get; }

        public KeyAttribute(string key)
        {
            this.Key = key;
        }
    }

    public static class KeyAttributeExtensions
    {
        public static string GetKey(this Type type)
        {
            return type.GetCustomAttribute<KeyAttribute>()?.Key ?? type.Name;
        }
    }
}