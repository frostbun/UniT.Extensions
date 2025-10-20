#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class RaycastExtensions
    {
        public static T? Raycast<T>(this Camera camera, Vector3 screenPosition, float maxDistance = Mathf.Infinity, LayerMask? layerMask = null)
        {
            if (!Physics.Raycast(camera.ScreenPointToRay(screenPosition), out var hit, maxDistance, layerMask ?? Physics.DefaultRaycastLayers)) return default;
            if (hit.rigidbody) return hit.rigidbody.GetComponentOrDefault<T>();
            if (hit.collider) return hit.collider.GetComponentInParentOrDefault<T>();
            return default;
        }

        public static T? Raycast2D<T>(this Camera camera, Vector3 screenPosition, float maxDistance = Mathf.Infinity, LayerMask? layerMask = null)
        {
            var hit = Physics2D.Raycast(camera.ScreenToWorldPoint(screenPosition), camera.transform.forward, maxDistance, layerMask ?? Physics2D.DefaultRaycastLayers);
            if (!hit) return default;
            if (hit.rigidbody) return hit.rigidbody.GetComponentOrDefault<T>();
            if (hit.collider) return hit.collider.GetComponentInParentOrDefault<T>();
            return default;
        }
    }
}