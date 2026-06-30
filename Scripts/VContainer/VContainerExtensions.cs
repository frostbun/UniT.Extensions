#nullable enable
namespace VContainer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using UniT.Extensions;
    using UnityEngine;
    using VContainer.Internal;
    using VContainer.Unity;
    using Object = UnityEngine.Object;

    public static class VContainerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder RegisterResource<T>(this IContainerBuilder builder, string path, Lifetime lifetime) where T : Object
        {
            return builder.Register(_ => LoadResource<T>(path), lifetime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComponentRegistrationBuilder RegisterComponentInNewPrefabResource<T>(this IContainerBuilder builder, string path, Lifetime lifetime) where T : Component
        {
            return builder.RegisterComponentInNewPrefab(_ => LoadResource<T>(path), lifetime);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RegistrationBuilder AsInterfacesAndSelf(this RegistrationBuilder registrationBuilder)
        {
            return registrationBuilder.AsImplementedInterfaces().AsSelf();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AutoResolve(this IContainerBuilder builder, Type type)
        {
            builder.RegisterBuildCallback(container => container.Resolve(type));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AutoResolve<T>(this IContainerBuilder builder) where T : notnull
        {
            builder.AutoResolve(typeof(T));
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Instantiate(this IObjectResolver container, Type type, IReadOnlyList<IInjectParameter> parameters)
        {
            return InjectorCache.GetOrBuild(type).CreateInstance(container, parameters);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this IObjectResolver container, IReadOnlyList<IInjectParameter> parameters) where T : notnull
        {
            return (T)container.Instantiate(typeof(T), parameters);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object Instantiate(this IObjectResolver container, Type type, params object?[] @params)
        {
            return container.Instantiate(type, (IReadOnlyList<IInjectParameter>)@params.Select(param => new Parameter(param)).ToArray());
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Instantiate<T>(this IObjectResolver container, params object?[] @params) where T : notnull
        {
            return (T)container.Instantiate(typeof(T), @params);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T LoadResource<T>(string path) where T : Object
        {
            return Resources.Load<T>(path).NullIfDestroyed() ?? throw new KeyNotFoundException($"{path} not found in Resources");
        }

        private sealed class Parameter : IInjectParameter
        {
            private readonly object? value;

            public Parameter(object? value) => this.value = value;

            bool IInjectParameter.Match(Type parameterType, string _) => parameterType.IsInstanceOfType(this.value);

            object? IInjectParameter.GetValue(IObjectResolver _) => this.value;
        }
    }
}