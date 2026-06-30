using System;
using _Project.Core.Math;
using _Project.Core.Physics.Movement;

namespace _Project.Core.Physics.Collision
{
    public struct CollisionData : IHasPosition
    {
        public readonly MovementModel modelA;
        public readonly MovementModel modelB;
        public readonly Vector2 collisionNormal;
        public readonly Vector2 contactPointPosition;
        public Vector2 Position => contactPointPosition;
        public event Action<Vector2> PositionChanged;


        public CollisionData(MovementModel modelA, MovementModel modelB, Vector2 collisionNormal, Vector2 contactPointPosition)
        {
            this.modelA = modelA;
            this.modelB = modelB;
            this.collisionNormal = collisionNormal;
            this.contactPointPosition = contactPointPosition;
            PositionChanged = null;
        }
    }
}