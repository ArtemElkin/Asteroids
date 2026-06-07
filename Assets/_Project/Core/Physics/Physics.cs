using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public static class Physics
    {
        public static Vector2 ApplyInertia(Vector2 velocity, float friction, float deltaTime)
        {
            if (velocity.sqrMagnitude < 0.001f) return Vector2.zero;
        
            var drag = velocity.normalized * friction * deltaTime;
            if (drag.sqrMagnitude > velocity.sqrMagnitude) return Vector2.zero;
            return velocity - drag;
        }
        
        public static Vector2 ApplyAcceleration(Vector2 velocity, Vector2 moveDirection, float acceleration, float deltaTime)
        {
            return velocity + moveDirection * acceleration * deltaTime;
        }

        public static Vector2 CalculateCollisionVelocity(Vector2 v1, Vector2 v2, float m1, float m2, Vector2 collisionNormal)
        {
            // to avoid division on null
            if (collisionNormal.magnitude == 0) return v1;
            
            return v1 - (2*m2 / (m1 + m2)) 
                * (Vector2.Dot(v1 - v2, collisionNormal) / collisionNormal.sqrMagnitude) 
                * collisionNormal;
        }
    }
}