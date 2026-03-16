#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
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
        private static readonly Dictionary<Type, string> Cache = new();

        public static string GetKey(this Type type)
        {
            return Cache.GetOrAdd(type, type => type.GetCustomAttribute<KeyAttribute>()?.Key ?? type.Name, type);
        }
    }
}