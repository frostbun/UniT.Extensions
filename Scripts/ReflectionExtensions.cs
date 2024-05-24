#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        public static IEnumerable<FieldInfo> GetAllFields(this Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        {
            return type.GetFields(bindingFlags)
                .Concat(type.BaseType is { } baseType
                    ? GetAllFields(baseType, bindingFlags)
                    : Enumerable.Empty<FieldInfo>()
                );
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        {
            return type.GetProperties(bindingFlags)
                .Concat(type.BaseType is { } baseType
                    ? GetAllProperties(baseType, bindingFlags)
                    : Enumerable.Empty<PropertyInfo>()
                );
        }

        public static bool IsBackingField(this FieldInfo field)
        {
            return field.Name.IsBackingFieldName();
        }

        public static bool IsBackingFieldName(this string str)
        {
            return str.StartsWith("<") && str.EndsWith(">k__BackingField");
        }

        public static string ToBackingFieldName(this string str)
        {
            return str.IsBackingFieldName() ? str : $"<{str}>k__BackingField";
        }

        public static string ToPropertyName(this string str)
        {
            return str.IsBackingFieldName() ? str.Substring(1, str.Length - 17) : str;
        }

        public static PropertyInfo? ToPropertyInfo(this FieldInfo field)
        {
            return field.DeclaringType?.GetProperty(field.Name.ToPropertyName());
        }

        public static bool DerivesFrom(this Type type, Type baseType)
        {
            return baseType.IsAssignableFrom(
                type.IsGenericType && baseType.ContainsGenericParameters
                    ? type.GetGenericTypeDefinition()
                    : type
            );
        }

        public static IEnumerable<Type> GetDerivedTypes(this Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(asm => !asm.IsDynamic)
                .SelectMany(asm => asm.GetTypes())
                .Where(type => !type.IsAbstract && baseType.IsAssignableFrom(type));
        }

        public static void CopyTo(this object from, object to)
        {
            from.GetType().GetAllFields()
                .Intersect(to.GetType().GetAllFields())
                .ForEach(field => field.SetValue(to, field.GetValue(from)));
        }
    }
}