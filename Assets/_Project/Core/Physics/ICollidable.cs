using System;
using _Project.Core.Math;

namespace _Project.Core.Physics
{
    public interface ICollidable
    {
        event Action<Vector2> OnCollided;
        void ActivateCollision();
        void DeactivateCollision();
    }
}