using UnityEngine;

namespace CodeBase.Utils
{
    public static class CameraExtensions
    {
        public static Vector2 WorldToNdc(this Camera camera, Vector3 worldPosition) =>
            (Vector2)camera.WorldToViewportPoint(worldPosition) * 2f - Vector2.one;
    }
}