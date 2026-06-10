using System;
using _Project.Core.EventBus;
using _Project.Core.Factories;
using _Project.Features.Common.Event;

namespace _Project.Features.Spaceship.Weapon.Projectile
{
    public class ProjectileSpawner : IDisposable
    {
        private readonly IFactory<ProjectileSpawnData, ProjectileFacade> _factory;
        private readonly IEventBus _eventBus;


        public ProjectileSpawner(
            IFactory<ProjectileSpawnData, ProjectileFacade> factory,
            IEventBus eventBus)
        {
            _factory = factory;
            _eventBus = eventBus;
            _eventBus.Subscribe<SpawnRequestedEvent<ProjectileFacade>>(OnSpawnRequested);
        }

        private void OnSpawnRequested(SpawnRequestedEvent<ProjectileFacade> @event)
        {
            var initialMovementData = @event.initialMovementData;
            var spawnData = new ProjectileSpawnData(initialMovementData);
            _factory.Create(spawnData);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<SpawnRequestedEvent<ProjectileFacade>>(OnSpawnRequested);
        }
    }
}