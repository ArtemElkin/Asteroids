using UnityEngine;
using CoreVector2 = _Project.Core.Math.Vector2;

namespace _Project.Infrastructure.UnityServices
{
    public static class VectorExtensions
    {
        public static Vector2 ToUnity(this CoreVector2 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static CoreVector2 ToCore(this Vector2 v)
        {
            return new CoreVector2(v.x, v.y);
        }

        public static CoreVector2 ToCore(this Vector3 v)
        {
            return new CoreVector2(v.x, v.y);
        }
    }
}