using _Project.Core.EventBus;
using _Project.Core.Physics;
using _Project.Features.Asteroid.Config;
using _Project.Features.Common.Event;

namespace _Project.Features.Asteroid
{
    public class AsteroidDestructor
    {
        private readonly MovementModel _movementModel;
        private readonly int _fragmentsCount;
        private readonly IEventBus _eventBus;
        
        
        public AsteroidDestructor(
            MovementModel movementModel,
            int fragmentsCount,
            IEventBus eventBus)
        {
            _movementModel = movementModel;
            _fragmentsCount = fragmentsCount;
            _eventBus = eventBus;
        }

        public void Destruct(AsteroidFacade self, bool fullDestruct)
        {
            if (!fullDestruct)
            {
                for (int i = 0; i < _fragmentsCount; i++)
                {
                    var initialPosition = _movementModel.Position;
                    var initialVelocity = _movementModel.Velocity;
                    var fragmentMass = _movementModel.Mass / _fragmentsCount;
                    var initialMovementData = new InitialMovementData(fragmentMass, initialPosition, initialVelocity);
                    _eventBus.Publish(new SpawnRequestedEvent<AsteroidFacade>(initialMovementData));
                }
            }
            _eventBus.Publish(new DespawnRequestedEvent<AsteroidFacade>(self));
        }
    }
}