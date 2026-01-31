#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        [Pure]
        public static ConstructorInfo GetSingleConstructor(this Type type)
        {
            return type.GetConstructors() switch
            {
                { Length: 0 }    => throw new InvalidOperationException($"No constructor found for {type.Name}"),
                { Length: > 1 }  => throw new InvalidOperationException($"Multiple constructors found for {type.Name}"),
                { } constructors => constructors[0],
            };
        }

        [Pure]
        public static Type GetSingleDerivedType(this Type type)
        {
            return type.GetDerivedTypes().ToArray() switch
            {
                { Length: 0 }   => throw new InvalidOperationException($"No derived type found for {type.Name}"),
                { Length: > 1 } => throw new InvalidOperationException($"Multiple derived types found for {type.Name}"),
                { } types       => types[0],
            };
        }

        [Pure]
        public static Func<object> GetEmptyConstructor(this Type type)
        {
            var constructor = type.GetConstructors().SingleOrDefault(constructor => constructor.GetParameters().All(parameter => parameter.HasDefaultValue))
                ?? type.GetSingleConstructor();
            var parameters = constructor.GetParameters().Select(parameter => parameter.HasDefaultValue ? parameter.DefaultValue : null).ToArray();
            return () => constructor.Invoke(parameters);
        }

        [Pure]
        public static IEnumerable<FieldInfo> GetAllFields(this Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        {
            return (type.BaseType?.GetAllFields(bindingFlags) ?? Enumerable.Empty<FieldInfo>()).Concat(type.GetFields(bindingFlags));
        }

        [Pure]
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        {
            return (type.BaseType?.GetAllProperties(bindingFlags) ?? Enumerable.Empty<PropertyInfo>()).Concat(type.GetProperties(bindingFlags));
        }

        [Pure]
        public static IEnumerable<Type> GetDerivedTypes(this Type baseType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(asm => !asm.IsDynamic)
                .SelectMany(baseType.GetDerivedTypes);
        }

        [Pure]
        public static IEnumerable<Type> GetDerivedTypes(this Type baseType, Assembly assembly)
        {
            return assembly.GetTypes().Where(type => !type.IsAbstract && baseType.IsAssignableFrom(type));
        }

        [Pure]
        public static bool IsAssignableTo(this Type type, Type baseType)
        {
            return baseType.IsAssignableFrom(type);
        }

        public static void CopyTo(this object from, object to)
        {
            from.GetType().GetAllFields()
                .Intersect(to.GetType().GetAllFields())
                .ForEach(field => field.SetValue(to, field.GetValue(from)));
        }

        [Pure]
        public static bool IsBackingField(this FieldInfo field)
        {
            return field.Name.IsBackingFieldName();
        }

        [Pure]
        public static bool IsBackingFieldName(this string str)
        {
            return str.StartsWith("<") && str.EndsWith(">k__BackingField");
        }

        [Pure]
        public static string ToBackingFieldName(this string str)
        {
            return str.IsBackingFieldName() ? str : $"<{str}>k__BackingField";
        }

        [Pure]
        public static string ToPropertyName(this string str)
        {
            return str.IsBackingFieldName() ? str.Substring(1, str.Length - 17) : str;
        }

        [Pure]
        public static FieldInfo? ToBackingFieldInfo(this PropertyInfo property)
        {
            return property.DeclaringType?.GetField(property.Name.ToBackingFieldName());
        }

        [Pure]
        public static PropertyInfo? ToPropertyInfo(this FieldInfo backingField)
        {
            return backingField.DeclaringType?.GetProperty(backingField.Name.ToPropertyName());
        }
    }
}