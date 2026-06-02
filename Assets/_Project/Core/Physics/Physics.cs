using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public static class Physics
    {
        public static float ApplyInertia(float speed, float inertiaMultiplier, float deltaTime)
        {
            return speed * (1 - inertiaMultiplier * deltaTime);
        }
        
        public static float ApplyAcceleration(float speed, float accelerationMultiplier, float deltaTime)
        {
            return speed + accelerationMultiplier * deltaTime;
        }
    }
}