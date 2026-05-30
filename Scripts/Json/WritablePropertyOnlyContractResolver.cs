#nullable enable
namespace UniT.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public sealed class WritablePropertyOnlyContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);

            var constructors = type.GetConstructors();

            var constructor = constructors.SingleOrDefault(constructor => constructor.GetCustomAttribute<JsonConstructorAttribute>() is not null)
                ?? constructors.MaxByOrDefault(constructor => constructor.GetParameters().Length);

            if (constructor is null) return properties.Where(property => property.Writable).ToArray();

            var propertyNames = constructor.GetParameters()
                .Select(parameter => parameter.GetCustomAttribute<JsonPropertyAttribute>() is { PropertyName: { } name } ? name : parameter.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            return properties
                .Where((property, propertyNames) => property.Writable || propertyNames.Contains(property.PropertyName!), propertyNames)
                .ToArray();
        }
    }
}