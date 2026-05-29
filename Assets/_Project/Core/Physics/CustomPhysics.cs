using _Project.Core.Math;


namespace _Project.Core.Physics
{
    public static class CustomPhysics
    {
        public static CustomVector2 ApplyInertia(CustomVector2 velocity, float inertiaMultiplier, float deltaTime)
        {
            return velocity * (1 - inertiaMultiplier * deltaTime);
        }
        
        public static CustomVector2 ApplyAcceleration(CustomVector2 velocity, float accelerationMultiplier, CustomVector2 direction, float deltaTime)
        {
            return velocity + accelerationMultiplier * deltaTime * direction;
        }
    }
}