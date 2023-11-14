namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionExtensions
    {
        public static FieldInfo[] GetAllFields(this Type type, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
        {
            return type.GetFields(bindingFlags);
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
            return str.IsBackingFieldName() ? str[1..^16] : str;
        }

        public static PropertyInfo ToPropertyInfo(this FieldInfo field)
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
            foreach (var fromField in from.GetType().GetAllFields())
            {
                var toField = to.GetType().GetField(fromField.Name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                if (toField is null) return;
                if (!toField.FieldType.IsAssignableFrom(fromField.FieldType)) return;
                toField.SetValue(to, fromField.GetValue(from));
            }
        }
    }
}