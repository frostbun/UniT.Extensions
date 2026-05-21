#nullable enable
namespace UniT.Extensions
{
    using System.Diagnostics.Contracts;
    using UnityEngine;

    public static class PhysicsExtensions
    {
        [Pure]
        public static T? Raycast<T>(this Camera camera, Vector3 screenPosition, float maxDistance = Mathf.Infinity, LayerMask? layerMask = null)
        {
            if (!Physics.Raycast(camera.ScreenPointToRay(screenPosition), out var hit, maxDistance, layerMask ?? Physics.DefaultRaycastLayers)) return default;
            if (hit.rigidbody) return hit.rigidbody.GetComponentOrDefault<T>();
            if (hit.collider) return hit.collider.GetComponentInParentOrDefault<T>();
            return default;
        }
    }
}