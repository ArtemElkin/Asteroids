using _Project.Core.Physics;
using _Project.Core.Signals;
using _Project.Features.Asteroid.Config;
using _Project.Features.Common.Signals;

namespace _Project.Features.Asteroid
{
    public class AsteroidDestructor
    {
        private readonly MovementModel _movementModel;
        private readonly AsteroidConfig _config;
        private readonly ISignalBus _signalBus;
        
        
        public AsteroidDestructor(
            MovementModel movementModel, 
            AsteroidConfig config,
            ISignalBus signalBus)
        {
            _movementModel = movementModel;
            _config = config;
            _signalBus = signalBus;
        }

        public void Destruct(AsteroidFacade self)
        {
            for (int i = 0; i < _config.fragmentsCount; i++)
            {
                var initialPosition = _movementModel.Position;
                var initialVelocity = _movementModel.Velocity;
                var fragmentMass = _movementModel.Mass / _config.fragmentsCount;
                var initialMovementData = new InitialMovementData(fragmentMass, initialPosition, initialVelocity);
                _signalBus.Fire(new SpawnRequestedSignal<AsteroidFacade>(initialMovementData));
            }

            if (_config.fragmentsCount > 0)
            {
                _signalBus.Fire(new CloneDespawnRequestedSignal<AsteroidFacade>(self));
            }
            _signalBus.Fire(new DespawnRequestedSignal<AsteroidFacade>(self));
        }
    }
}