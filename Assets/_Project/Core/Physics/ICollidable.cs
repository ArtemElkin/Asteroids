using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface ICollidable
    {
        void Setup(MovementModel movementModel);
        void Reset();
        MovementModel MovementModel { get; }
        event Action<ICollidable, Vector2> OnCollided;
        void ActivateCollision();
        void DeactivateCollision();
    }
}