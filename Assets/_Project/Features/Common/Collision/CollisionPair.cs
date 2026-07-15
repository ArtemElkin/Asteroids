using System;
using _Project.Core.Physics.Movement;

namespace _Project.Features.Common.Collision
{
    public readonly struct CollisionPair : IEquatable<CollisionPair>
    {
        private readonly MovementModel _modelA;
        private readonly MovementModel _modelB;
        
        
        public CollisionPair(MovementModel modelA, MovementModel modelB)
        {
            _modelA = modelA;
            _modelB = modelB;
        }

        public bool Equals(CollisionPair other)
        {
            return (_modelA == other._modelA && _modelB == other._modelB)
                   || (_modelA == other._modelB && _modelB == other._modelA);
        }

        public override bool Equals(object obj) => obj is CollisionPair other && Equals(other);
        
        public override int GetHashCode() => _modelA.GetHashCode() ^ _modelB.GetHashCode();
        
    }
}