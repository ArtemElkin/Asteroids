namespace _Project.Core.Physics
{
    public static class Physics
    {
        public static float ApplyInertia(float speed, float friction, float deltaTime)
        {
            var newSpeed = speed - friction * deltaTime;
            return newSpeed < 0f ? 0f : newSpeed;
        }
        
        public static float ApplyAcceleration(float speed, float accelerationMultiplier, float deltaTime)
        {
            return speed + accelerationMultiplier * deltaTime;
        }
    }
}