using CandyMasters.Project.Scripts.Objects.Implementations.Entities.Camera;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Common.Extensions
{
    public static class Vector3Extension
    {
        public static Vector3 ToScreen(this Vector3 world, ActiveCamera camera) => camera.GetScreenPosition(world);
    }
}