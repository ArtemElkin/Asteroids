using System;
using _Project.Core.Physics;
using _Project.Core.Signals;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidDestructor
    {
        private readonly MovementModel _movementModel;
        private readonly int _fragmentsCount;
        private readonly ISignalBus _signalBus;
        
        
        public AsteroidDestructor(
            int fragmentsCount,
            MovementModel movementModel,
            ISignalBus signalBus)
        {
            _fragmentsCount = fragmentsCount;
            _movementModel = movementModel;
            _signalBus = signalBus;
        }

        public void Destruct(AsteroidFacade self)
        {
            for (int i = 0; i < _fragmentsCount; i++)
            {
                var initialPosition = _movementModel.Position;
                var initialVelocity = _movementModel.Velocity;
                var fragmentMass = _movementModel.Mass / _fragmentsCount;
                var initialMovementData = new InitialMovementData(fragmentMass, initialPosition, initialVelocity);
                _signalBus.Fire(new SpawnRequestedSignal<AsteroidFacade>(initialMovementData));
            }

            if (_fragmentsCount > 0)
            {
                _signalBus.Fire(new CloneDespawnRequestedSignal<AsteroidFacade>(self));
            }
            _signalBus.Fire(new DespawnRequestedSignal<AsteroidFacade>(self));
        }
    }
}