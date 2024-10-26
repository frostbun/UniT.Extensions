#if UNIT_VCONTAINER
#nullable enable
namespace VContainer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VContainer.Internal;
    using VContainer.Unity;
    using Object = UnityEngine.Object;

    public static class VContainerExtensions
    {
        public static RegistrationBuilder RegisterResource<T>(this IContainerBuilder builder, string path, Lifetime lifetime) where T : Object
        {
            return builder.Register(_ => Resources.Load<T>(path) ?? throw new ArgumentOutOfRangeException(nameof(path), path, $"Failed to load {path}"), lifetime);
        }

        public static ComponentRegistrationBuilder RegisterComponentInNewPrefabResource<T>(this IContainerBuilder builder, string path, Lifetime lifetime) where T : Component
        {
            return builder.RegisterComponentInNewPrefab(_ => Resources.Load<T>(path) ?? throw new ArgumentOutOfRangeException(nameof(path), path, $"Failed to load {path}"), lifetime);
        }

        public static RegistrationBuilder AsInterfacesAndSelf(this RegistrationBuilder registrationBuilder)
        {
            return registrationBuilder.AsImplementedInterfaces().AsSelf();
        }

        public static void AutoResolve(this IContainerBuilder builder, Type type)
        {
            builder.RegisterBuildCallback(container => container.Resolve(type));
        }

        public static void AutoResolve<T>(this IContainerBuilder builder)
        {
            builder.AutoResolve(typeof(T));
        }

        public static object Instantiate(this IObjectResolver container, Type type, IReadOnlyList<IInjectParameter> parameters)
        {
            return InjectorCache.GetOrBuild(type).CreateInstance(container, parameters);
        }

        public static T Instantiate<T>(this IObjectResolver container, IReadOnlyList<IInjectParameter> parameters)
        {
            return (T)container.Instantiate(typeof(T), parameters);
        }

        public static object Instantiate(this IObjectResolver container, Type type, params object?[] @params)
        {
            return container.Instantiate(type, (IReadOnlyList<IInjectParameter>)@params.Select(param => new Parameter(param)).ToArray());
        }

        public static T Instantiate<T>(this IObjectResolver container, params object?[] @params)
        {
            return (T)container.Instantiate(typeof(T), @params);
        }
    }

    public sealed class Parameter : IInjectParameter
    {
        private readonly object? value;

        public Parameter(object? value) => this.value = value;

        bool IInjectParameter.Match(Type parameterType, string _) => parameterType.IsInstanceOfType(this.value);

        object? IInjectParameter.GetValue(IObjectResolver _) => this.value;
    }
}
#endif