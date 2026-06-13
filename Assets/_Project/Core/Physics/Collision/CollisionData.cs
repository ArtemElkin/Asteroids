using _Project.Core.Math;
using _Project.Core.Physics.Movement;

namespace _Project.Core.Physics.Collision
{
    public struct CollisionData
    {
        public readonly MovementModel modelA;
        public readonly MovementModel modelB;
        public readonly Vector2 collisionNormal;


        public CollisionData(MovementModel modelA, MovementModel modelB, Vector2 collisionNormal)
        {
            this.modelA = modelA;
            this.modelB = modelB;
            this.collisionNormal = collisionNormal;
        }
    }
}