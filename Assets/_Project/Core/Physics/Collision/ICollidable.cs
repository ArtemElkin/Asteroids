using System;
using _Project.Core.Physics.Movement;

namespace _Project.Core.Physics.Collision
{
    public interface ICollidable
    {
        void Setup(MovementModel movementModel);
        void Reset();
        MovementModel MovementModel { get; }
        event Action<CollisionData> OnCollided;
        void ActivateCollision();
        void DeactivateCollision();
    }
}