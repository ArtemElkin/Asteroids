using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public static class Physics
    {
        public static Vector2 ApplyInertia(Vector2 velocity, float inertiaMultiplier, float deltaTime)
        {
            return velocity * (1 - inertiaMultiplier * deltaTime);
        }
        
        public static Vector2 ApplyAcceleration(Vector2 velocity, float accelerationMultiplier, Vector2 direction, float deltaTime)
        {
            return velocity + accelerationMultiplier * deltaTime * direction;
        }
    }
}