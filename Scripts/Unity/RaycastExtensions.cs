#nullable enable
namespace UniT.Extensions
{
    using UnityEngine;

    public static class RaycastExtensions
    {
        public static T? Raycast<T>(this Camera camera, Vector3 position, float maxDistance = Mathf.Infinity, LayerMask? layerMask = null)
        {
            return Physics.Raycast(
                camera.ScreenPointToRay(position),
                out var hit,
                maxDistance,
                layerMask ?? Physics.DefaultRaycastLayers
            )
                ? hit.GetComponent<T>()
                : default;
        }

        public static T? Raycast<T>(this Camera camera, float maxDistance = Mathf.Infinity, LayerMask? layerMask = null)
        {
            return camera.Raycast<T>(Input.mousePosition, maxDistance, layerMask);
        }

        public static T? Raycast<T>(this Camera camera, Touch touch, float maxDistance = Mathf.Infinity, LayerMask? layerMask = null)
        {
            return camera.Raycast<T>(touch.position, maxDistance, layerMask);
        }

        private static T? GetComponent<T>(this RaycastHit hit)
        {
            return hit.rigidbody
                && hit.rigidbody.TryGetComponent<T>(out var result)
                || hit.collider.TryGetComponentInParent<T>(out result)
                    ? result
                    : default;
        }
    }
}